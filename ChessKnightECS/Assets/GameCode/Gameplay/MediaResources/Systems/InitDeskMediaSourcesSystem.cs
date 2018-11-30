using System.Collections.Generic;
using Fwt.Core;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Ck.Gameplay
{
  [UpdateInGroup(typeof(GameLoop.InitializeGroup))]
  public class InitDeskMediaSourcesSystem : ComponentSystem
  {
    struct Added
    {
      public readonly int Length;
      [ReadOnly] public EntityArray Entity;
      [ReadOnly] public SharedComponentDataArray<DeskMediaResources> Resources;
      public SubtractiveComponent<DeskMediaResourcesSorted> NoSortedResources;
    }

    struct Removed
    {
      public readonly int Length;
      [ReadOnly] public EntityArray Entity;
      [ReadOnly] public SharedComponentDataArray<DeskMediaResourcesSorted> SortedResources;
      public SubtractiveComponent<DeskMediaResources> NoMediaResources; 
    }

    [Inject] Added added;
    [Inject] Removed removed;

    protected override void OnUpdate()
    {
      for (int i = 0; i < added.Length; i++)
      {
        var resources = added.Resources[i];
        
        var sortedDeskItems = new Dictionary<DeskItemTypes, GameObject[]>();

        sortedDeskItems.Add(DeskItemTypes.Armor, resources.Armor);
        sortedDeskItems.Add(DeskItemTypes.BackgroundDark, resources.BackgroundDark); 
        sortedDeskItems.Add(DeskItemTypes.BackgroundLight, resources.BackgroundLight); 
        sortedDeskItems.Add(DeskItemTypes.FigureBishop, resources.Bishop); 
        sortedDeskItems.Add(DeskItemTypes.Bomb, resources.Bomb); 
        sortedDeskItems.Add(DeskItemTypes.Goal, resources.Goal); 
        sortedDeskItems.Add(DeskItemTypes.FigureKing, resources.King); 
        sortedDeskItems.Add(DeskItemTypes.FigureKnight, resources.Knight); 
        sortedDeskItems.Add(DeskItemTypes.MoveTarget, resources.MoveTarget); 
        sortedDeskItems.Add(DeskItemTypes.FigurePawn, resources.Pawn); 
        sortedDeskItems.Add(DeskItemTypes.PlayerUnit, resources.PlayerUnit); 
        sortedDeskItems.Add(DeskItemTypes.FigureQueen, resources.Queen); 
        sortedDeskItems.Add(DeskItemTypes.FigureRook, resources.Rook); 

        var sorted = new DeskMediaResourcesSorted {
          DeskItems = sortedDeskItems
        };

        PostUpdateCommands.AddSharedComponent(added.Entity[i], sorted);
      }

      for (int i = 0; i < removed.Length; i++)
      {
        PostUpdateCommands.RemoveComponent<DeskMediaResourcesSorted>(removed.Entity[i]);
      }
    }
  }
}
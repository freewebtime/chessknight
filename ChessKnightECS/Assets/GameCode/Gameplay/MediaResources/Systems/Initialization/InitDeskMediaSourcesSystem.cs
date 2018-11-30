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
        var figures = new Dictionary<ChessFigureTypes, GameObject[]>();
        figures[ChessFigureTypes.Bishop] = resources.Bishop;

        var sorted = new DeskMediaResourcesSorted {
          Figures = figures
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
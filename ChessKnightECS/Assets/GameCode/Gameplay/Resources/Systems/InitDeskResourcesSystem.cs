using System.Collections.Generic;
using Fwt.Core;
using Unity.Collections;
using Unity.Entities;

namespace Ck.Gameplay
{
  [UpdateInGroup(typeof(GameLoop.InitializeGroup))]
  public class InitDeskResourcesSystem : ComponentSystem
  {
    struct InitDeskCache: ISystemStateComponentData {}

    struct Added
    {
      public readonly int Length;
      [ReadOnly] public EntityArray Entity;
      [ReadOnly] public SharedComponentDataArray<DeskResources> DeskContent;
      public SubtractiveComponent<InitDeskCache> NoCache;
    }

    struct Removed 
    {
      public readonly int Length;
      [ReadOnly] public EntityArray Entity;
      [ReadOnly] public ComponentDataArray<InitDeskCache> Cache;
      public SubtractiveComponent<DeskResources> NoContent;
    }

    [Inject] Added added;
    [Inject] Removed removed;

    protected override void OnUpdate()
    {
      for (int i = 0; i < added.Length; i++)
      {
        var entity = added.Entity[i];
        var deskContent = added.DeskContent[i];
        var cellPrefabs = deskContent.CellPrefabs;

        var sorted = new Dictionary<DeskItemTypes, DeskItemPrefab>();
        for (int k = 0; k < cellPrefabs.Length; k++)
        {
          var cellPrefab = cellPrefabs[k];
          sorted[cellPrefab.DeskItemType] = cellPrefab;
        }

        deskContent.CellPrefabsSorted = sorted;
        
        PostUpdateCommands.SetSharedComponent(entity, deskContent);
        PostUpdateCommands.AddComponent(entity, new InitDeskCache());
      }

      for (int i = 0; i < removed.Length; i++)
      {
        PostUpdateCommands.RemoveComponent<InitDeskCache>(removed.Entity[i]);
      }
    }
  }
}
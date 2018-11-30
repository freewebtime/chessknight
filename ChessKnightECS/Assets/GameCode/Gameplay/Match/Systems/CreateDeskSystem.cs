using Fwt.Core;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Ck.Gameplay
{
  [UpdateInGroup(typeof(GameLoop.InitializeGroup))]
  public class CreateMatchDeskSystem : ComponentSystem
  {
    struct CreateDeskCache: ISystemStateSharedComponentData
    {
      public Entity DeskEntity;
      public GameObject DeskGameObject;
    }

    struct Added
    {
      public readonly int Length;
      [ReadOnly] public EntityArray Entity;
      [ReadOnly] public ComponentDataArray<Match> Match;
      [ReadOnly] public SharedComponentDataArray<MatchConfig> MatchConfig; 
      public SubtractiveComponent<CreateDeskCache> NoCache;
    }

    struct Removed
    {
      public readonly int Length;
      [ReadOnly] public EntityArray Entity;
      [ReadOnly] public SubtractiveComponent<Match> NoMatch;
      [ReadOnly] public SubtractiveComponent<MatchConfig> NoMatchConfig; 
      [ReadOnly] public SharedComponentDataArray<CreateDeskCache> Cache;
    }

    [Inject] Added added;
    [Inject] Removed removed;

    [Inject] DataResourcesApi dataResourcesApi;

    protected override void OnUpdate()
    {
      UpdateAdded();
      UpdateRemoved();
    }

    private void UpdateAdded() {
      if (added.Length == 0) {
        return;
      }

      var deskResources = dataResourcesApi.GetDeskResources();
      if (!deskResources.HasValue) {
        return;        
      }

      var deskPrefab = deskResources.Value.DeskPrefab;
      if (deskPrefab == null) {
        return;
      }

      var matchEntities = new NativeArray<Entity>(added.Length, Allocator.Temp);
      added.Entity.CopyTo(matchEntities);

      for (int i = 0; i < matchEntities.Length; i++)
      {
        var matchEntity = matchEntities[i];
        var deskGo = UnityEngine.Object.Instantiate(deskPrefab);
        deskGo.name = string.Format("Match desk");
        var deskGoEntity = deskGo.GetComponent<GameObjectEntity>();
        var deskEntity = deskGoEntity.Entity;

        var matchConfig = EntityManager.GetSharedComponentData<MatchConfig>(matchEntity);

        PostUpdateCommands.AddSharedComponent(deskEntity, matchConfig.DeskConfig);

        PostUpdateCommands.AddSharedComponent(matchEntity, new CreateDeskCache {
          DeskEntity = deskEntity,
          DeskGameObject = deskGo
        });

        PostUpdateCommands.AddComponent(matchEntity, new DeskReference {
          Target = deskEntity
        });
        PostUpdateCommands.AddComponent(deskEntity, new MatchReference {
          Target = matchEntity
        });
      }

      matchEntities.Dispose();
    }

    private void UpdateRemoved() 
    {
      var removedGroup = GetComponentGroup(
        ComponentType.Create<CreateDeskCache>(),
        ComponentType.Subtractive<Match>(),
        ComponentType.Subtractive<MatchConfig>()
      );
      var removedEntities = removedGroup.GetEntityArray();
      var cacheArray = removedGroup.GetSharedComponentDataArray<CreateDeskCache>();
      var entityArray = removedGroup.GetEntityArray();

      for (int i = 0; i < removedEntities.Length; i++)
      {
        PostUpdateCommands.RemoveComponent<CreateDeskCache>(entityArray[i]);

        // destroy desk game object
        var deskGo = cacheArray[i].DeskGameObject;
        UnityEngine.Object.Destroy(deskGo);
      }
    }
  }
}
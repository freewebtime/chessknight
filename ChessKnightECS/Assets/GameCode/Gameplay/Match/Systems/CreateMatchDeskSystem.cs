using Fwt.Core;
using Unity.Collections;
using Unity.Entities;

namespace Ck.Gameplay
{
  [UpdateInGroup(typeof(GameLoop.InitializeGroup))]
  public class CreateMatchDeskSystem : ComponentSystem
  {
    struct CreateMatchDeskCache: ISystemStateComponentData
    {
      public Entity DeskEntity;
    }

    struct Added
    {
      public readonly int Length;
      [ReadOnly] public EntityArray Entity;
      [ReadOnly] public ComponentDataArray<Match> Match;
      [ReadOnly] public SharedComponentDataArray<MatchConfig> MatchConfig; 
      public SubtractiveComponent<CreateMatchDeskCache> NoCache;
    }

    struct Removed
    {
      public readonly int Length;
      [ReadOnly] public EntityArray Entity;
      [ReadOnly] public SubtractiveComponent<Match> NoMatch;
      [ReadOnly] public SubtractiveComponent<MatchConfig> NoMatchConfig; 
      [ReadOnly] public ComponentDataArray<CreateMatchDeskCache> Cache;
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

      var deskEntities = new NativeArray<Entity>(added.Length, Allocator.Temp);
      EntityManager.Instantiate(deskPrefab, deskEntities);

      for (int i = 0; i < matchEntities.Length; i++)
      {
        var matchEntity = matchEntities[i];
        var deskEntity = deskEntities[i];

        var matchConfig = EntityManager.GetSharedComponentData<MatchConfig>(matchEntity);

        PostUpdateCommands.AddSharedComponent(deskEntity, matchConfig.DeskConfig);

        PostUpdateCommands.AddComponent(matchEntity, new CreateMatchDeskCache {
          DeskEntity = deskEntity
        });

        PostUpdateCommands.AddComponent(matchEntity, new DeskReference {
          Target = deskEntity
        });
        PostUpdateCommands.AddComponent(deskEntity, new MatchReference {
          Target = matchEntity
        });

      }

      matchEntities.Dispose();
      deskEntities.Dispose();
    }

    private void UpdateRemoved() 
    {
      var removedGroup = GetComponentGroup(
        ComponentType.Create<CreateMatchDeskCache>(),
        ComponentType.Subtractive<Match>(),
        ComponentType.Subtractive<MatchConfig>()
      );
      var removedEntities = removedGroup.GetEntityArray();
      var cacheArray = removedGroup.GetComponentDataArray<CreateMatchDeskCache>();
      var entityArray = removedGroup.GetEntityArray();

      for (int i = 0; i < removedEntities.Length; i++)
      {
        var deskEntity = cacheArray[i].DeskEntity;
        if (EntityManager.Exists(deskEntity)) {
          PostUpdateCommands.DestroyEntity(deskEntity);
        }

        PostUpdateCommands.RemoveComponent<CreateMatchDeskCache>(entityArray[i]);
      }
    }
  }
}
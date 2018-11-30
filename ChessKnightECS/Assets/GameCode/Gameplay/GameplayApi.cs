using Unity.Collections;
using Unity.Entities;

namespace Ck.Gameplay
{
  public class GameplayApi : ComponentSystem
  {
    struct MatchGroup
    {
      public readonly int Length;
      [ReadOnly] public EntityArray Entity;
      [ReadOnly] public ComponentDataArray<Match> Match;
    }

    [Inject] DataResourcesApi resourcesApi;

    [Inject] MatchGroup matchGroup;

    public Match? GetCurrentMatch()
    {
      if (matchGroup.Length > 0) {
        return matchGroup.Match[0];
      }

      return null;
    }

    public Entity? StartMatch(MatchConfig matchConfig)
    {
      // get match prefab
      var matchResources = resourcesApi.GetMatchResources();
      if (!matchResources.HasValue) {
        return null;
      }

      // get desk prefab
      var deskResources = resourcesApi.GetDeskResources();
      if (!deskResources.HasValue) {
        return null;
      }

      // stop current match if any
      StopMatch();

      // instantiate match prefab
      var matchEntity = EntityManager.Instantiate(matchResources.Value.MatchPrefab);
      EntityManager.AddSharedComponentData(matchEntity, matchConfig);
    
      // instantiate game desk
      var deskEntity = EntityManager.Instantiate(deskResources.Value.DeskPrefab);
      
      // add references from desk to match and from match to desk
      EntityManager.AddComponentData(deskEntity, new MatchReference {
        Target = matchEntity
      });
      EntityManager.AddComponentData(matchEntity, new DeskReference {
        Target = deskEntity
      });

      // instantiate desk items
      var deskItemsPrefabs = matchConfig.DeskConfig.DeskItems;
      for (int k = 0; k < deskItemsPrefabs.Length; k++)
      {
        var deskItemConfig = deskItemsPrefabs[k];
        if (deskItemConfig.Prefab == null) {
          continue;
        }

        // create DeskItem entity
        var deskItemEntity = EntityManager.Instantiate(deskItemConfig.Prefab);

        // add references from desk item to match and to desk
        EntityManager.AddComponentData(deskItemEntity, new DeskReference {
          Target = deskEntity
        });
        EntityManager.AddComponentData(deskItemEntity, new MatchReference {
          Target = matchEntity
        });

        // set coordinate
        EntityManager.SetComponentData(deskItemEntity, new Coordinate {
          Value = deskItemConfig.Coordinate
        });

      }

      return matchEntity;
    }

    public void StopMatch()
    {
      for (int i = 0; i < matchGroup.Length; i++)
      {
        EntityManager.DestroyEntity(matchGroup.Entity[i]);
      }
    }

    protected override void OnUpdate() {}
  }
}
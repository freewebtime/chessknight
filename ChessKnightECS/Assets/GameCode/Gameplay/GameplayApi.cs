using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Ck.Gameplay
{
  public class GameplayApi : ComponentSystem
  {
    [Inject] GameResourcesApi resourcesApi;

    public Entity? GetCurrentMatch()
    {
      var matchGroup = GetMatchGroup();

      if (matchGroup.Length > 0) {
        return matchGroup.Entity[0];
      }

      return null;
    }

    public Entity? StartMatch(MatchConfig matchConfig)
    {
      var gameResources = resourcesApi.GetGameResources();
      if (!gameResources.HasValue || gameResources.Value.Matches.Length == 0) {
        return null;
      }

      var matchesResources = gameResources.Value.Matches;
      
      var matchResourcesId = matchConfig.MatchResourcesId;
      if (matchResourcesId < 0 || matchesResources.Length <= matchResourcesId) {
        return null;
      }

      // get match prefab
      var matchResources = matchesResources[matchResourcesId];
      var matchPrefab = matchResources.MatchPrefab;

      if (matchPrefab == null) {
        return null;
      }

      // stop current match if any
      StopMatch();

      // instantiate match prefab
      var matchEntity = EntityManager.Instantiate(matchPrefab);
      EntityManager.AddSharedComponentData(matchEntity, matchConfig);
      EntityManager.AddSharedComponentData(matchEntity, matchResources);
    
      return matchEntity;
    }

    public void StopMatch()
    {
      var matchGroup = GetMatchGroup();

      for (int i = 0; i < matchGroup.Length; i++)
      {
        // destroy match
        EntityManager.DestroyEntity(matchGroup.Entity[i]);
      }
    }

    public struct MatchGroup
    {
      public int Length;
      public EntityArray Entity;
    }
    
    public MatchGroup GetMatchGroup()
    {
      var matchGroup = GetComponentGroup(
        ComponentType.Create<Match>()
      );

      var entityArray = matchGroup.GetEntityArray();
      var result = new MatchGroup {
        Length = entityArray.Length,
        Entity = entityArray,
      };
  
      return result;
    }

    protected override void OnUpdate() {}
  }
}
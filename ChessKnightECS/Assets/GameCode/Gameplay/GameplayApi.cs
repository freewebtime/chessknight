using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Ck.Gameplay
{
  public class GameplayApi : ComponentSystem
  {
    [Inject] DataResourcesApi resourcesApi;

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
      // get match prefab
      var matchResources = resourcesApi.GetMatchResources();
      if (!matchResources.HasValue) {
        return null;
      }

      // stop current match if any
      StopMatch();

      // instantiate match prefab
      var matchEntity = EntityManager.Instantiate(matchResources.Value.MatchPrefab);
      EntityManager.AddSharedComponentData(matchEntity, matchConfig);
    
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
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

      // stop current match if any
      StopMatch();

      // instantiate match prefab
      var matchEntity = EntityManager.Instantiate(matchResources.Value.MatchPrefab);
      EntityManager.AddSharedComponentData(matchEntity, matchConfig);
    
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
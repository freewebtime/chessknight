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

    [Inject] ResourcesApi resourcesApi;

    [Inject] MatchGroup matchGroup;

    public Match? GetCurrentMatch()
    {
      if (matchGroup.Length > 0) {
        return matchGroup.Match[0];
      }

      return null;
    }

    public void StartMatch(MatchConfig matchConfig)
    {
      // get match prefab
      var matchResources = resourcesApi.GetMatchResources();
      if (!matchResources.HasValue) {
        return;
      }

      // instantiate match prefab
      var matchEntity = EntityManager.Instantiate(matchResources.Value.MatchPrefab);
      EntityManager.AddSharedComponentData(matchEntity, matchConfig);
    }

    public void StartMatchNow()
    {
      var matchConfig = new MatchConfig {
        Desk = new DeskConfig {
          DeskItems = new DeskItemConfig[] {}
        }
      };
      StartMatch(matchConfig);
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
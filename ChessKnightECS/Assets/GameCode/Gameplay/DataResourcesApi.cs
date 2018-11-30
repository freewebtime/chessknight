using Unity.Collections;
using Unity.Entities;

namespace Ck.Gameplay
{
  public class DataResourcesApi : ComponentSystem
  {
    public MatchDataResources? GetMatchResources()
    {
      var group = GetComponentGroup(
        ComponentType.Create<MatchDataResources>()
      );
      var resources = group.GetSharedComponentDataArray<MatchDataResources>();
      if (resources.Length == 0)
      {
        return null;
      }

      var result = resources[0];
      return result;
    }

    public DeskDataResources? GetDeskResources()
    {
      var group = GetComponentGroup(
        ComponentType.Create<DeskDataResources>()
      );
      var resources = group.GetSharedComponentDataArray<DeskDataResources>();
      if (resources.Length == 0)
      {
        return null;
      }

      var result = resources[0];
      return result;
    }

    public SceneBackDataResources? GetSceneResources()
    {
      var group = GetComponentGroup(
        ComponentType.Create<SceneBackDataResources>()
      );
      var resources = group.GetSharedComponentDataArray<SceneBackDataResources>();
      if (resources.Length == 0)
      {
        return null;
      }

      var result = resources[0];
      return result;

    }

    protected override void OnUpdate() {}
  }
}
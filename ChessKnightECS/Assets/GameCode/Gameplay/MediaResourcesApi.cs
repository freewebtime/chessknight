using Unity.Collections;
using Unity.Entities;

namespace Ck.Gameplay
{
  public class MediaResourcesApi: ComponentSystem
  {
    public SceneBackMediaResources? GetSceneResources()
    {
      var group = GetComponentGroup(
        ComponentType.Create<SceneBackMediaResources>()
      );
      var resources = group.GetSharedComponentDataArray<SceneBackMediaResources>();
      if (resources.Length == 0)
      {
        return null;
      }

      var result = resources[0];
      return result;
    }

    public DeskMediaResources? GetDeskResources()
    {
      var group = GetComponentGroup(
        ComponentType.Create<DeskMediaResources>()
      );
      var resources = group.GetSharedComponentDataArray<DeskMediaResources>();
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
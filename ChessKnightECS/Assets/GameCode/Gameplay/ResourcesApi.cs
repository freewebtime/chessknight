using Unity.Collections;
using Unity.Entities;

namespace Ck.Gameplay
{
  public class ResourcesApi : ComponentSystem
  {
    struct MatchResourcesGroup
    {
      public readonly int Length;
      [ReadOnly] public EntityArray Entity;
      [ReadOnly] public SharedComponentDataArray<MatchDataResources> Resources;
    }

    struct DeskResourcesGroup
    {
      public readonly int Length;
      [ReadOnly] public EntityArray Entity;
      [ReadOnly] public SharedComponentDataArray<DeskDataResources> Resources;
    }

    struct SceneResourcesGroup
    {
      public readonly int Length;
      [ReadOnly] public EntityArray Entity;
      [ReadOnly] public SharedComponentDataArray<SceneBackDataResources> Resources;
    }

    [Inject] MatchResourcesGroup matchResourcesGroup;
    [Inject] DeskResourcesGroup deskResourcesGroup;
    [Inject] SceneResourcesGroup sceneResourcesGroup;

    public MatchDataResources? GetMatchResources()
    {
      var group = matchResourcesGroup;
      
      if (group.Length == 0)
      {
        return null;
      }

      var result = group.Resources[0];
      return result;
    }

    public DeskDataResources? GetDeskResources()
    {
      var group = deskResourcesGroup;
      
      if (group.Length == 0)
      {
        return null;
      }

      var result = group.Resources[0];
      return result;
    }

    public SceneBackDataResources? GetSceneResources()
    {
      var group = sceneResourcesGroup;
      
      if (group.Length == 0)
      {
        return null;
      }

      var result = group.Resources[0];
      return result;
    }

    protected override void OnUpdate() {}
  }
}
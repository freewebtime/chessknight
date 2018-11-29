using Unity.Collections;
using Unity.Entities;

namespace Ck.Gameplay
{
  public class ResourcesApi : ComponentSystem
  {
    struct GamestateResourcesGroup
    {
      public readonly int Length;
      [ReadOnly] public EntityArray Entity;
      [ReadOnly] public SharedComponentDataArray<GamestateResources> Resources;
    }

    struct MatchResourcesGroup
    {
      public readonly int Length;
      [ReadOnly] public EntityArray Entity;
      [ReadOnly] public SharedComponentDataArray<MatchResources> Resources;
    }

    struct DeskResourcesGroup
    {
      public readonly int Length;
      [ReadOnly] public EntityArray Entity;
      [ReadOnly] public SharedComponentDataArray<DeskResources> Resources;
    }

    struct SceneResourcesGroup
    {
      public readonly int Length;
      [ReadOnly] public EntityArray Entity;
      [ReadOnly] public SharedComponentDataArray<SceneResources> Resources;
    }

    [Inject] GamestateResourcesGroup gamestateResourcesGroup;
    [Inject] MatchResourcesGroup matchResourcesGroup;
    [Inject] DeskResourcesGroup deskResourcesGroup;
    [Inject] SceneResourcesGroup sceneResourcesGroup;

    public GamestateResources? GetGamestateResources()
    {
      var group = gamestateResourcesGroup;
      
      if (group.Length == 0)
      {
        return null;
      }

      var result = group.Resources[0];
      return result;
    }

    public MatchResources? GetMatchResources()
    {
      var group = matchResourcesGroup;
      
      if (group.Length == 0)
      {
        return null;
      }

      var result = group.Resources[0];
      return result;
    }

    public DeskResources? GetDeskResources()
    {
      var group = deskResourcesGroup;
      
      if (group.Length == 0)
      {
        return null;
      }

      var result = group.Resources[0];
      return result;
    }

    public SceneResources? GetSceneResources()
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
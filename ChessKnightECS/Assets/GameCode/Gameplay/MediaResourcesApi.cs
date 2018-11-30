using Unity.Collections;
using Unity.Entities;

namespace Ck.Gameplay
{
  public class MediaResourcesApi: ComponentSystem
  {
    struct SceneBackResourcesGroup
    {
      public readonly int Length;
      [ReadOnly] public EntityArray Entity;
      [ReadOnly] public SharedComponentDataArray<SceneBackMediaResources> Resources;
    }

    struct DeskResourcesGroup
    {
      public readonly int Length;
      [ReadOnly] public EntityArray Entity;
      [ReadOnly] public SharedComponentDataArray<DeskMediaResources> Resources;
    }

    [Inject] SceneBackResourcesGroup sceneBackResourcesGroup;
    [Inject] DeskResourcesGroup deskResourcesGroup;

    public SceneBackMediaResources? GetSceneResources()
    {
      var group = sceneBackResourcesGroup;
      
      if (group.Length == 0)
      {
        return null;
      }

      var result = group.Resources[0];
      return result;
    }

    public DeskMediaResources? GetDeskResources()
    {
      var group = deskResourcesGroup;
      
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
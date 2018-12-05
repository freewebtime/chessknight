using Ck.Resources;
using Unity.Entities;

namespace Ck
{
  public class GameResourcesApi : ComponentSystem
  {
    public GameResources? GetGameResources()
    {
      var resourcesGroup = GetComponentGroup(
        ComponentType.Create<GameResources>()
      );

      var gameResourcesArray = resourcesGroup.GetSharedComponentDataArray<GameResources>();

      if (gameResourcesArray.Length == 0) {
        return null;
      }

      return gameResourcesArray[0];
    }

    protected override void OnUpdate() {}
  }
}
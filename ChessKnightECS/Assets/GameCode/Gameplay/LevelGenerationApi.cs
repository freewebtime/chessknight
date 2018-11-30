using Unity.Entities;

namespace Ck.Gameplay
{
  public class LevelGenerationApi : ComponentSystem
  {
    [Inject] ResourcesApi resourcesApi;

    public MatchConfig? GenerateRandomMatch()
    {
      var deskItems = new DeskItemConfig[0];
      var deskConfig = new DeskConfig {
        DeskItems = deskItems
      };
      var result = new MatchConfig {
        Desk = deskConfig
      };

      return default;
    }

    public MatchConfig? GenerateClassicChessKnightMatch()
    {
      return default;
    }

    protected override void OnUpdate() {}
  }
}
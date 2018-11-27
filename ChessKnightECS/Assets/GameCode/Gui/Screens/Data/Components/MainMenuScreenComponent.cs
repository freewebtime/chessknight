using Unity.Entities;

namespace Ck.Gui
{
  public class MainMenuScreenComponent: GuiScreenTypeComponent<MainMenuScreen> 
  {

    public void OnPlayNowClicked()
    {
      GameApi.PlayRandomLevelNow();
    }

    public void OnExitClicked()
    {
      GameApi.ExitGame();
    }

  }
}
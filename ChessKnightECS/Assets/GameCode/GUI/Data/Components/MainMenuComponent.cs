namespace Ck.Data
{
  public class MainMenuComponent: GuiComponentBase
  {
    public void ShowLevelboard()
    {
      GameApi.Navigate_Levelboard();
    }

    public void ExitGame()
    {
      GameApi.ExitGame();
    }
  }
}
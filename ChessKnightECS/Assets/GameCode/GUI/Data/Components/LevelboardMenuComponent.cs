namespace Ck.Data
{
  public class LevelboardMenuComponent: GuiComponentBase
  {
    public void ExitToMainMenu()
    {
      GameApi.Navigate_MainMenu();
    }

    public void PlayRandom()
    {
      GameApi.Play_RandomLevel();
    }
  }
}
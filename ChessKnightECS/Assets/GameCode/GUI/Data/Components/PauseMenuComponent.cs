namespace Ck.Data
{
  public class PauseMenuComponent: GuiComponentBase
  {
    public void ResumeGame()
    {
      GameApi.Gameplay_Resume();
    }

    public void ExitGame()
    {
      GameApi.Navigate_Levelboard();
    }

    public void RestartGame()
    {
      GameApi.Gameplay_Restart();
    }
  }
}
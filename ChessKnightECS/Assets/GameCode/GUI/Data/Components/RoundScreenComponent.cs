namespace Ck.Data
{
  public class RoundScreenComponent: GuiComponentBase
  {
    public void PauseGame()
    {
      GameApi.Gameplay_Pause();
    }
  }
}
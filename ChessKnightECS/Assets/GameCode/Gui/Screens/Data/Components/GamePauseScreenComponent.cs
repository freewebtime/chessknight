using Unity.Entities;

namespace Ck.Gui
{
  public class GamePauseScreenComponent: GuiScreenTypeComponent<GamePauseScreen> 
  {
    public void OnResumeClicked()
    {
      GameApi.ResumeGame();
    }

    public void OnExitClicked() 
    {
      GameApi.StopGame();
    }
  }
}
using System;
using Ck.Gui;
using Unity.Entities;

namespace Ck
{
  public class GameApi : ComponentSystem
  {
    
    [Inject] GuiApi guiApi;

    public void PlayRandomLevelNow()
    {
      guiApi.HideAllScreens();
      guiApi.ShowGuiScreen<MatchScreen>();
    }

    public void ExitGame()
    {
      guiApi.HideAllScreens();
    }

    public void PauseGame()
    {
      guiApi.HideAllScreens();
      guiApi.ShowGuiScreen<GamePauseScreen>();
    }

    public void ResumeGame()
    {
      guiApi.HideAllScreens();
      guiApi.ShowGuiScreen<MatchScreen>();
    }

    public void ExitToMainMenu()
    {
      guiApi.HideAllScreens();
      guiApi.ShowGuiScreen<MainMenuScreen>();
    }

    protected override void OnUpdate() {}
  }
}
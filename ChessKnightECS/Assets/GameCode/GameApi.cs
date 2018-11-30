using System;
using Ck.Gameplay;
using Ck.Gui;
using Unity.Collections;
using Unity.Entities;

namespace Ck
{
  public class GameApi : ComponentSystem
  {
    [Inject] GuiApi guiApi;
    [Inject] GameplayApi gameplayApi;

    public void PlayRandomLevelNow()
    {
      gameplayApi.StartMatchNow();

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
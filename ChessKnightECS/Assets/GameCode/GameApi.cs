using System;
using Ck.Gameplay;
using Ck.Gui;
using Unity.Collections;
using Unity.Entities;

namespace Ck
{
  public class GameApi : ComponentSystem
  {
    
    struct GamestateGroup
    {
      public readonly int Length;
      [ReadOnly] public ComponentDataArray<Gamestate> Gamestate;
    }

    [Inject] GuiApi guiApi;
    [Inject] GamestateGroup gamestateGroup;

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
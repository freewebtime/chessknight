using System;
using Ck.Gameplay;
using Ck.Gui;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Ck
{
  public class GameApi : ComponentSystem
  {
    [Inject] GuiApi guiApi;
    [Inject] GameplayApi gameplayApi;
    [Inject] LevelGenerationApi levelGenerationApi;

    public void PlayRandomLevelNow()
    {
      // generate match config
      var levelSize = new int2(10, 10);
      var randomSeed = 100500u;
      var matchConfig = levelGenerationApi.GenerateRandomMatch(levelSize, randomSeed);

      if (!matchConfig.HasValue) {
        return;
      }

      var matchEntity = gameplayApi.StartMatch(matchConfig.Value);

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

    public void StopGame()
    {
      gameplayApi.StopMatch();

      guiApi.HideAllScreens();
      guiApi.ShowGuiScreen<MainMenuScreen>();
    }

    protected override void OnUpdate() {}
  }
}
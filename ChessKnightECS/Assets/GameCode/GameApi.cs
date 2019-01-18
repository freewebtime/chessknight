using System;
using Ck.Data;
using Ck.Logic;
using Fwt.Core;
using Fwt.Core.Gui;
using Unity.Entities;
using UnityEngine;

namespace Ck
{
  [UpdateInGroup(typeof(GameLoop.ProcessInputGroup))]
  public class GameApi : ComponentSystem
  {
    [Inject] GuiWidgetsApi guiWidgetsApi;
    [Inject] GuiScreenApi guiScreenApi;

    public void Navigate_MainMenu() 
    {
      guiScreenApi.HideAllScreens();
      guiWidgetsApi.ShowWidget<GuiScreenLobby>(); 
      guiWidgetsApi.ShowWidget<GuiScreenMainMenu>(); 
    }

    public void Navigate_Levelboard() 
    {
      guiScreenApi.HideAllScreens();
      guiWidgetsApi.ShowWidget<GuiScreenLobby>(); 
      guiWidgetsApi.ShowWidget<GuiScreenLevelboard>(); 
    }

    public void Play_RandomLevel()
    {
      guiScreenApi.HideAllScreens();
      guiWidgetsApi.ShowWidget<GuiScreenRound>(); 
    }

    public void ExitGame()
    {
      guiScreenApi.HideAllScreens();
      Application.Quit();
    }

    public void Gameplay_Pause()
    {
      guiScreenApi.HideAllScreens();
      guiWidgetsApi.ShowWidget<GuiScreenPauseMenu>(); 
    }

    public void Gameplay_Resume()
    {
      guiScreenApi.HideAllScreens();
      guiWidgetsApi.ShowWidget<GuiScreenRound>(); 
    }

    public void Gameplay_Restart()
    {
      guiScreenApi.HideAllScreens();
      guiWidgetsApi.ShowWidget<GuiScreenRound>(); 
    }

    protected override void OnUpdate()
    {
    }
  }
}
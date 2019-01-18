using System;
using Ck.Data;
using Ck.Logic;
using Fwt.Core.Gui;
using Unity.Entities;
using UnityEngine;

namespace Ck
{
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
      Debug.Log("Play random level");

      guiScreenApi.HideAllScreens();
      guiWidgetsApi.ShowWidget<GuiScreenRound>(); 
    }

    public void ExitGame()
    {
      Debug.Log("Application quit");
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
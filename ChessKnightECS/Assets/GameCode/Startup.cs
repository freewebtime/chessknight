using Ck.Gui;
using Unity.Entities;
using UnityEngine;

public class Startup: MonoBehaviour 
{
  public void Start()
  {
    ShowMainMenu();
  }

  [ContextMenu("Show Main Menu")]
  private void ShowMainMenu()
  {
    var world = World.Active;
    var entityManager = world.GetOrCreateManager<EntityManager>();
    var guiApi = world.GetExistingManager<GuiApi>();

    guiApi.ShowGuiScreen<MainMenuScreen>();
  }
}
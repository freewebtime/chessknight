using Fwt.Core;
using Unity.Entities;

namespace Ck.Gui
{
  [UpdateInGroup(typeof(GuiLoop.CollectInputGroup))]
  public class GuiApi : ComponentSystem
  {

    public void ShowGuiScreen<TScreen>() where TScreen: struct
    {
      EntityManager.CreateEntity(
        ComponentType.Create<Command>(),
        ComponentType.Create<ShowGuiScreenCommand<TScreen>>()
      );
    }
    
    public void HideGuiScreen<TScreen>() where TScreen: struct
    {
      EntityManager.CreateEntity(
        ComponentType.Create<Command>(),
        ComponentType.Create<HideGuiScreenCommand<TScreen>>()
      );
    }

    public void HideAllGuiScreens<TScreen>() where TScreen: struct
    {
      EntityManager.CreateEntity(
        ComponentType.Create<Command>(),
        ComponentType.Create<HideAllGuiScreensCommand>()
      );
    }

    protected override void OnUpdate() {}
  }
}
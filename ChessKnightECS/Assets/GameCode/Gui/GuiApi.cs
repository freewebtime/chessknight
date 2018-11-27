using Fwt.Core;
using Unity.Entities;

namespace Ck.Gui
{
  public class GuiApi : ComponentSystem
  {

    [Inject] GuiLoop.GuiCollectInputBarrier collectInputBarrier;

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

    public void HideAllScreens()
    {
      EntityManager.CreateEntity(
        ComponentType.Create<Command>(),
        ComponentType.Create<HideAllGuiScreensCommand>()
      );
    }

    protected override void OnUpdate() {}
    
  }
}
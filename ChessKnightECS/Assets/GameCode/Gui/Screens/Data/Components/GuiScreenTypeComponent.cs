using Unity.Entities;

namespace Ck.Gui
{
  public abstract class GuiScreenTypeComponent<TScreen>: ComponentDataWrapper<GuiScreenType<TScreen>> where TScreen: struct {}
}
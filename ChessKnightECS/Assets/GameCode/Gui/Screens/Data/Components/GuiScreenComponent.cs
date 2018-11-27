using Unity.Entities;

namespace Ck.Gui
{
  public abstract class GuiScreenComponent<TScreen>: ComponentDataWrapper<GuiScreen<TScreen>> where TScreen: struct {}
}
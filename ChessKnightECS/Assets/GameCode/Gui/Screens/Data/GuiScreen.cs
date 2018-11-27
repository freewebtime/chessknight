using Unity.Entities;

namespace Ck.Gui
{
  public struct GuiScreen<TScreen>: IComponentData where TScreen: struct {}
}
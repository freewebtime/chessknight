using Unity.Entities;

namespace Ck.Gui
{
  public struct GuiScreenType<TScreen>: IComponentData where TScreen: struct {}
}
using Unity.Entities;

namespace Ck.Gui
{
  public struct ShowGuiScreenCommand<TScreen>: IComponentData where TScreen: struct {}
}
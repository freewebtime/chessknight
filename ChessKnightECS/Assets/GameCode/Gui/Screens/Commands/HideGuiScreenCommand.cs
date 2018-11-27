using Unity.Entities;

namespace Ck.Gui
{
  public struct HideGuiScreenCommand<TScreen>: IComponentData where TScreen: struct {}
}
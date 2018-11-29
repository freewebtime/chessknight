using Unity.Entities;

namespace Ck.Gameplay
{
  public abstract class DeskItemTypeWrapper<TDeskItem>: ComponentDataWrapper<TDeskItem> where TDeskItem: struct, IComponentData {}
}
using Unity.Entities;

namespace Ck.Gameplay
{
  public abstract class DeskItemWrapperBase<TDeskItem>: ComponentDataWrapper<TDeskItem> where TDeskItem: struct, IComponentData {}
}
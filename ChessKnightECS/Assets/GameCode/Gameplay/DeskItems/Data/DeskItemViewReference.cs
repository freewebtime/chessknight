using Unity.Entities;

namespace Ck.Gameplay
{
  public struct DeskItemViewReference: IComponentData
  {
    public Entity Target;
  }
}
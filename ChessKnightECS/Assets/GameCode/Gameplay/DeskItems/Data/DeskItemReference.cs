using Unity.Entities;

namespace Ck.Gameplay
{
  public struct DeskItemReference: IComponentData
  {
    public Entity Target;
  }
}
using Unity.Entities;

namespace Ck.Gameplay
{
  public struct DeskReference: IComponentData
  {
    public Entity Target;
  }
}
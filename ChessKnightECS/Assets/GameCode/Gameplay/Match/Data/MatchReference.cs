using Unity.Entities;

namespace Ck.Gameplay
{
  public struct MatchReference: IComponentData
  {
    public Entity Target;
  }
}
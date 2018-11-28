using Unity.Entities;

namespace Ck.Gameplay
{

  public partial class ChessFigures
  {
    public struct Pawn: IComponentData {}
    public struct Rook: IComponentData {}
    public struct Knight: IComponentData {}
    public struct Bishop: IComponentData {}
    public struct Queen: IComponentData {}
    public struct King: IComponentData {}
  }

}

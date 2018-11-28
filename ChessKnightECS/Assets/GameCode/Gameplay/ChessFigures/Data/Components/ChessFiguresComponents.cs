using Unity.Entities;

namespace Ck.Gameplay
{
  public partial class ChessFigures
  {
    public class PawnComponent: ComponentDataWrapper<Pawn> {}
    public class RookComponent: ComponentDataWrapper<Rook> {}
    public class KnightComponent: ComponentDataWrapper<Knight> {}
    public class BishopComponent: ComponentDataWrapper<Bishop> {}
    public class QueenComponent: ComponentDataWrapper<Queen> {}
    public class KingComponent: ComponentDataWrapper<King> {}
  }

}
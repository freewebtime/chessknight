using Unity.Entities;

namespace ChessKnight.Components
{
    public struct Coordinate2d: IComponentData
    {
        public int X;
        public int Y;
    }
}

using Unity.Entities;

namespace ChessKnight.Gameplay
{
    public class Grid2dCoordinateComponent: ComponentDataWrapper<Grid2dCoordinate> { }

    public struct Grid2dCoordinate: IComponentData
    {
        public int X;
        public int Y;
    }
}

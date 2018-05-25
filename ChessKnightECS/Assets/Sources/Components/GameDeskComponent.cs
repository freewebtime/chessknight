using Unity.Entities;

namespace ChessKnight.Components
{
    public struct GameDeskComponent: IComponentData
    {
        public int Width;
        public int Height;
    }
}

using Unity.Entities;

namespace ChessKnight.Gameplay
{
    public class GameDeskSizeComponent: ComponentDataWrapper<GameDeskSize> {}

    public struct GameDeskSize : IComponentData
    {
        public int Width;
        public int Height;
    }
}

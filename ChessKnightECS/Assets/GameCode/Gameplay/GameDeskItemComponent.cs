using Unity.Entities;

namespace ChessKnight.Gameplay
{
    public class GameDeskItemComponent: ComponentDataWrapper<GameDeskItem> { }

    public struct GameDeskItem : IComponentData
    {
    }
}

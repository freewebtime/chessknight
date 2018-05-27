using Unity.Entities;

namespace ChessKnight.Gameplay
{
    public class GameDeskComponent: ComponentDataWrapper<GameDesk> { }

    public struct GameDesk : IComponentData
    {
    }
}

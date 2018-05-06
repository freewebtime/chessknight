using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace ChessKnight.Unity
{
    [Game, Unique]
    public class GameRootComponent: IComponent
    {
        public GameRootScript Value;
    }
}

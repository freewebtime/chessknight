using ChessKnight.Unity.AssetsManagament;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace ChessKnight.Unity
{
    [Game, Unique]
    public class GraphicsPackComponent: IComponent
    {
        public GraphicsPackConfig Value;
    }
}

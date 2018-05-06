using ChessKnight.Unity;
using Entitas;

namespace ChessKnight.View
{
    [Game]
    public class ViewComponent: IComponent
    {
        public ViewScript Value;
        public ViewScript Prefab;
    }
}

using ChessKnight.Unity;
using Entitas;

namespace ChessKnight.View
{
    [Game]
    public class ViewPrefabComponent: IComponent
    {
        public ViewScript Value;
    }
}

using ChessKnight.View.Systems;

namespace ChessKnight.View
{
    public class ViewFeature : Feature
    {
        public ViewFeature(Contexts contexts) : base("View")
        {
            Add(new SetViewSystem(contexts.game));
        }
    }
}

using ChessKnight.View.Systems;

namespace ChessKnight.View
{
    public class ViewFeature : Feature
    {
        public ViewFeature(Contexts contexts) : base("View")
        {
            Add(new SetViewSystem(contexts.game));
            Add(new SetViewPositionSystem(contexts.game));
            Add(new SetViewRotationSystem(contexts.game));
            Add(new SetViewScaleSystem(contexts.game));
            Add(new SetViewParentSystem(contexts.game));
        }
    }
}

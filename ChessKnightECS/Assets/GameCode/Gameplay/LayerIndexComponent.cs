using Unity.Entities;

namespace ChessKnight.Gameplay
{
    public class LayerIndexComponent: ComponentDataWrapper<LayerIndex> { }

    public struct LayerIndex : IComponentData
    {
        public int Value;
    }
}

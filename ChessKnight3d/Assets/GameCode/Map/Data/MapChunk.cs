using Unity.Entities;
using Unity.Mathematics;

namespace Assets.GameCode.Map.Data
{
    public struct MapChunk: IComponentData
    {
        public int chunkIndex;
        public int2 coordinate;
        public int2 position;
        public int2 size;
    }
}

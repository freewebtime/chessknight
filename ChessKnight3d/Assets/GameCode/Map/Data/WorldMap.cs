using Unity.Entities;
using Unity.Mathematics;

namespace Assets.GameCode.Map.Data
{
    public struct WorldMap: IComponentData
    {
        public int2 mapSize;
        public int2 chunkSize;
        public int2 sizeInChunks;
    }
}

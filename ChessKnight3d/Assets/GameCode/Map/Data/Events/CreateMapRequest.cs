using Unity.Entities;
using Unity.Mathematics;

namespace Assets.GameCode.Map.Data.Events
{
    public struct CreateMapRequest: IComponentData
    {
        public int2 chunkSize;
        public int2 sizeInChunks;
        public int mapResourcesIndex;
    }
}

using Assets.GameCode.Shared;
using Unity.Entities;
using Unity.Mathematics;

namespace Assets.GameCode.Map.Data.Events
{
    public struct GenerateMapRequest: IComponentData
    {
        public float2 noiseScale;
        public int randomSeed;
        public Booleans isFlatMap;
        public byte seaLevel;
    }
}

using Unity.Collections;
using Unity.Entities;

namespace Assets.GameCode.Map.Data
{
    public struct Heightmap : ISharedComponentData
    {
        public NativeArray<byte> height00;
        public NativeArray<byte> height11;
        public NativeArray<byte> height10;
        public NativeArray<byte> height01;
        public NativeArray<float> center;
    }
}

using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Assets.GameCode.Map.Data
{
    public struct Itemsmap: ISharedComponentData
    {
        public NativeArray<int> id;
        public NativeArray<float3> position;
        public NativeArray<float3> rotation;
        public NativeArray<float3> scale;
    }
}

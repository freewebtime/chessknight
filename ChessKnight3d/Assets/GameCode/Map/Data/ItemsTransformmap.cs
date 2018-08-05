using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.GameCode.Map.Data
{
    public struct ItemsTransformmap: ISharedComponentData
    {
        public NativeArray<Matrix4x4> matrix;
        public NativeArray<float3> position;
        public NativeArray<float3> rotation;
        public NativeArray<float3> scale;
    }
}

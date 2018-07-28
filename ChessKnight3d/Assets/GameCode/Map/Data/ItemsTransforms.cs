using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Assets.GameCode.Map.Data
{
    public struct ItemsTransforms: ISharedComponentData
    {
        public NativeArray<Matrix4x4> value;
    }
}

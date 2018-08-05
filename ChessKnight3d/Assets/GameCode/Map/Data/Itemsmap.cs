using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Assets.GameCode.Map.Data
{
    public struct Itemsmap: ISharedComponentData
    {
        public NativeArray<int> value;
    }
}

using Unity.Collections;
using Unity.Entities;

namespace Assets.GameCode.Map.Data
{
    public struct Chunksmap: ISharedComponentData
    {
        public NativeArray<Entity> value;
    }
}

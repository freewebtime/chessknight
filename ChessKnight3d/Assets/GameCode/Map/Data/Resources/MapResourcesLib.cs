using Unity.Collections;
using Unity.Entities;

namespace Assets.GameCode.Map.Data.Resources
{
    public struct MapResourcesLib: ISharedComponentData
    {
        public NativeArray<Entity> mapPacks;
    }
}

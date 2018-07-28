using Assets.GameCode.Configs.Map;
using Unity.Entities;

namespace Assets.GameCode.Map.Data.Events
{
    public struct InitMapResourcesLibRequest: ISharedComponentData
    {
        public MapResourcesLibConfig config;
    }
}

using Assets.GameCode.Configs.Map;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Assets.GameCode.Map.Data.Resources
{
    public struct MapResourcePack: ISharedComponentData
    {
        public Material groundMaterial;
        public Material waterMapterial;
        public NativeArray<Entity> mapItems;
        public NativeArray<GroundTypeConfig> groundTypeConfigs;
    }

}

using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.GameCode.Map.Data.Resources
{
    public struct MapItemResourcePack: ISharedComponentData
    {
        public int id;
        public Material material;
        public Mesh mesh;

        public float3 position;
        public float3 rotation;
        public float3 scale;
    }
}

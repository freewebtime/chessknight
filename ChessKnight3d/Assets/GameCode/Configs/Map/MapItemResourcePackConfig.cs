using Unity.Mathematics;
using UnityEngine;

namespace Assets.GameCode.Configs.Map
{
    [CreateAssetMenu]
    public class MapItemResourcePackConfig: ScriptableObject
    {
        public int id;
        public Material material;
        public Mesh mesh;

        public float3 position;
        public float3 rotation;
        public float3 scale;
    }
}

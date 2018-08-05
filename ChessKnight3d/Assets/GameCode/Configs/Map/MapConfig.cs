using UnityEngine;

namespace Assets.GameCode.Configs.Map
{
    [CreateAssetMenu]
    public class MapConfig: ScriptableObject
    {
        public int id;

        public Material groundMaterial;
        public Material waterMaterial;

        public MapItemConfig[] mapItems;
        public GroundTypeConfig[] groundTypes;
    }
}

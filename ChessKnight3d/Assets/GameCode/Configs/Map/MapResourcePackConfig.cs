using UnityEngine;

namespace Assets.GameCode.Configs.Map
{
    [CreateAssetMenu]
    public class MapResourcePackConfig: ScriptableObject
    {
        public int id;
        public Material groundMaterial;
        public Material waterMaterial;
        public MapItemResourcePackConfig[] mapItems;
        public GroundTypeConfig[] groundTypeConfigs;
        public Sprite[] groundTypeSprites;
    }
}

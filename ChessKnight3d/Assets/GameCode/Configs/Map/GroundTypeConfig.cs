using Assets.GameCode.Shared;
using UnityEngine;

namespace Assets.GameCode.Configs.Map
{
    [CreateAssetMenu]
    public class GroundTypeConfig: ScriptableObject 
    {
        public int id;
        public SpriteUv uv;
        public Sprite sprite;
    }
}

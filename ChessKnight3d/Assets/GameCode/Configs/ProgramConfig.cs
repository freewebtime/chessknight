using Assets.GameCode.Configs.Map;
using UnityEngine;

namespace Assets.GameCode.Configs
{
    [CreateAssetMenu]
    public class ProgramConfig: ScriptableObject
    {
        public static ProgramConfig config;

        public MapConfig[] mapConfigs;
    }
}

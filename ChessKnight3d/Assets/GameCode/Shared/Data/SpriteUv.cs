using System;
using Unity.Mathematics;

namespace Assets.GameCode.Shared
{
    [Serializable]
    public struct SpriteUv
    {
        public float2 uv00;
        public float2 uv01;
        public float2 uv11;
        public float2 uv10;
    }
}

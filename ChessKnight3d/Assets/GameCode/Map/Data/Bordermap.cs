﻿using Unity.Collections;
using Unity.Entities;

namespace Assets.GameCode.Map.Data
{
    public struct Bordermap : ISharedComponentData
    {
        public NativeArray<byte> north;
        public NativeArray<byte> south;
        public NativeArray<byte> west;
        public NativeArray<byte> east;
    }
}

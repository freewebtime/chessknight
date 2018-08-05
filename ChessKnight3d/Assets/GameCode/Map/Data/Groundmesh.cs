using Unity.Entities;
using UnityEngine;

namespace Assets.GameCode.Map.Data
{
    public struct Groundmesh: ISharedComponentData
    {
        public Mesh value;
    }
}

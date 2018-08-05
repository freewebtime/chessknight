using Unity.Entities;
using UnityEngine;

namespace Assets.GameCode.Map.Data
{
    public struct Watermesh: ISharedComponentData
    {
        public Mesh value;
    }
}

using Unity.Entities;
using UnityEngine;

namespace Assets.GameCode.Map.Data
{
    public struct Bordermesh: ISharedComponentData
    {
        public Mesh value;
    }
}

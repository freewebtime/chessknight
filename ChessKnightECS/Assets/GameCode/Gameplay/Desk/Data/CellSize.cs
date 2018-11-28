using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Ck.Gameplay
{
  [Serializable]
  public struct CellSize: IComponentData
  {
    public float2 Value;
  }

}
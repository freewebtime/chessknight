using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Ck.Gameplay
{
  [Serializable]
  public struct Coordinate: IComponentData 
  {
    public int2 Value;
  }

}
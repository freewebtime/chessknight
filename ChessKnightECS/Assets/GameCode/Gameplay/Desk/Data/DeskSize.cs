using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Ck.Gameplay
{
  [Serializable]
  public struct DeskSize: IComponentData
  {
    public int2 Value;
  }


}
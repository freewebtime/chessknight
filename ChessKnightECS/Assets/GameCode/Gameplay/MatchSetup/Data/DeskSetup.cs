using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Ck.Gameplay
{
  [Serializable]
  public struct DeskSetup: ISharedComponentData
  {
    public int2 DeskSize;
  }
}
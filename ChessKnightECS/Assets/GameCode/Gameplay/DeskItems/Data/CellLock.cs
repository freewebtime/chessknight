using System;
using Unity.Entities;

namespace Ck.Gameplay
{
  [Serializable]
  public struct CellLock: IComponentData
  {
    public int Id;
  }

}
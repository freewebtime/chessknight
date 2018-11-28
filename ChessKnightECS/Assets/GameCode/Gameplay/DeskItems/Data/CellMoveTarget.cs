using System;
using Unity.Entities;

namespace Ck.Gameplay
{
  [Serializable]
  public struct CellMoveTarget: IComponentData
  {
    public int Id;
  }

}
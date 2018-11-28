using System;
using Unity.Entities;

namespace Ck.Gameplay
{
  [Serializable]
  public struct CellBomb: IComponentData
  {
    public int Id;
  }

}
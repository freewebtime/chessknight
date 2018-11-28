using System;
using Unity.Entities;

namespace Ck.Gameplay
{
  [Serializable]
  public struct CellGoal: IComponentData
  {
    public int Id;
  }

}
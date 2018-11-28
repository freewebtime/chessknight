using System;
using Unity.Entities;

namespace Ck.Gameplay
{
  [Serializable]
  public struct CellBackground: IComponentData 
  {
    public int Id;
  }

}
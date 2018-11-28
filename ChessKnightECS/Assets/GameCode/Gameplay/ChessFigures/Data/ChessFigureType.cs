using System;
using Unity.Entities;

namespace Ck.Gameplay
{
  [Serializable]
  public struct ChessFigureType: IComponentData 
  {
    public ChessFigureTypes Value;
  }
  

}
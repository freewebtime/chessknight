using System;
using Unity.Entities;

namespace Ck.Gameplay
{
  [Serializable]
  public struct FigureType: IComponentData 
  {
    public ChessFigureType Value;
  }

}
using System;
using Unity.Entities;

namespace Ck.Gameplay
{
  [Serializable]
  public struct ChessFigure: IComponentData
  {
    public ChessFigureTypes FigureType;
  }
}
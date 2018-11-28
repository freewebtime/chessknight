using System;
using Unity.Entities;

namespace Ck.Gameplay
{

  [Serializable]
  public struct Player: IComponentData
  {
    public int Index;
  }
  
}
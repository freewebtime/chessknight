using System;
using Unity.Entities;

namespace Ck.Gameplay
{
  [Serializable]
  public struct DeskItemViewId: IComponentData 
  {
    public int Id;
  }
}
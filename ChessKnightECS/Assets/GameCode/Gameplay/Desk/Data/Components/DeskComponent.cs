using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Ck.Gameplay
{

  // Desk
  public class DeskComponent: ComponentDataWrapper<Desk> {}

  [Serializable]
  public struct DeskSize: IComponentData
  {
    public int2 Value;
  }


}
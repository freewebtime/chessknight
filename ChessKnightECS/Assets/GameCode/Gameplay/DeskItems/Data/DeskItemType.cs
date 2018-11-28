using System;
using Unity.Entities;

namespace Ck.Gameplay
{
  [Serializable]
  public struct DeskItemType: IComponentData
  {
    public DeskItemTypes Value;
  }
}
using System;
using Unity.Entities;

namespace Ck.Gameplay
{
  [Serializable]
  public struct DeskReference: IComponentData
  {
    public Entity Value;
  }
}
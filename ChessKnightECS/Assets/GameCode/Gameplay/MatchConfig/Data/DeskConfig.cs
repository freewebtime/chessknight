using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Ck.Gameplay
{
  [Serializable]
  public struct DeskConfig: ISharedComponentData
  {
    public DeskItemConfig[] DeskItems;
  }
}
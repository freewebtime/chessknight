using System;
using System.Collections.Generic;
using Ck.Gameplay;
using Unity.Entities;

namespace Ck.Gameplay
{
  [Serializable]
  public struct DeskDataResources: ISharedComponentData
  {
    public DeskDataSkin DefaultDesk;
  }
}

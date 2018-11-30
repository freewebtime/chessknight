using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;

namespace Ck.Gameplay
{
  public struct DeskItemsListByCoord: ISharedComponentData
  {
    public Dictionary<int2, List<Entity>> Value;
  }

}
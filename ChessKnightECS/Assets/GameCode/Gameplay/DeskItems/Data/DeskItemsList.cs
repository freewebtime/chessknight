using System.Collections.Generic;
using Unity.Entities;

namespace Ck.Gameplay
{
  public struct DeskItemsList: ISharedComponentData
  {
    public List<Entity> Value;
  }

}
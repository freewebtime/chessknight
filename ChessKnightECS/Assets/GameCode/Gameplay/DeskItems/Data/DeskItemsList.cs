using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Ck.Gameplay
{
  public struct DeskItemsList: ISharedComponentData
  {
    public List<Entity> DeskItemsEntity;
    public List<GameObject> DeskItemGo;
  }

}
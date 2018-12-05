using System;
using System.Collections.Generic;
using Unity.Entities;

namespace Ck.Resources
{
  [Serializable]
  public struct DeskItemsResources: ISharedComponentData
  {
    public DeskItemsGroupResources[] DeskItemsGroups;
    public Dictionary<int, DeskItemsGroupResources> DeskItemsByItemType;
  }
}
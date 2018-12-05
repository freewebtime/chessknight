using System;
using Unity.Entities;

namespace Ck.Resources
{
  [Serializable]
  public struct DeskItemsResources: ISharedComponentData
  {
    public DeskItemsGroupResources[] DeskItemsGroups;
  }
}
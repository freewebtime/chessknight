using System;
using Unity.Entities;

namespace Ck.Resources
{
  [Serializable]
  public struct DeskItemsGroupResources: ISharedComponentData
  {
    public int GroupId;
    public DeskItemResources[] DeskItems;
  }
}
using System;
using Ck.Gameplay;
using Unity.Entities;

namespace Ck.Resources
{
  [Serializable]
  public struct DeskItemsGroupResources: ISharedComponentData
  {
    public string Name;
    public DeskItemTypes ItemsType;
    public DeskItemResources[] DeskItems;
  }
}
using System;
using Unity.Entities;
using UnityEngine;

namespace Ck.Resources
{
  [Serializable]
  public struct DeskResources: ISharedComponentData
  {
    public GameObject DeskPrefab;

    public DeskItemsResources DeskItems;
  }
}
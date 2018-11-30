using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Ck.Gameplay
{
  public struct DeskMediaResourcesSorted: ISharedComponentData
  {
    public Dictionary<DeskItemTypes, GameObject[]> DeskItems;
  }
}
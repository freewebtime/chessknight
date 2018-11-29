using System;
using System.Collections.Generic;
using Ck.Gameplay;
using Unity.Entities;
using UnityEngine;

namespace Ck.Gameplay
{
  [Serializable]
  public struct DeskResources: ISharedComponentData
  {
    public GameObject DeskPrefab;
  }
}

using System;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Ck.Gameplay
{
  [Serializable]
  public struct SceneBackMediaResources: ISharedComponentData
  {
    public GameObject[] Prefabs;
  }
}
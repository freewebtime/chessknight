using System;
using Unity.Entities;
using UnityEngine;

namespace Ck.Gameplay
{
  [Serializable]
  public struct ApplicationDataResources: ISharedComponentData
  {
    public GameObject ApplicationPrefab;
  }
}
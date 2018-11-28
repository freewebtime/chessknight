using System;
using Unity.Entities;
using UnityEngine;

namespace Ck.Gameplay
{
  [Serializable]
  public struct ApplicationResources: ISharedComponentData
  {
    public GameObject ApplicationPrefab;
  }
}
using System;
using Unity.Entities;
using UnityEngine;

namespace Ck.Gameplay
{
  [Serializable]
  public struct PlayerResources: ISharedComponentData
  {
    public GameObject PlayerPrefab;
  }
}
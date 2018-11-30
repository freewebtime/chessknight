using System;
using Unity.Entities;
using UnityEngine;

namespace Ck.Gameplay
{
  [Serializable]
  public struct PlayerDataResources: ISharedComponentData
  {
    public GameObject PlayerPrefab;
  }
}
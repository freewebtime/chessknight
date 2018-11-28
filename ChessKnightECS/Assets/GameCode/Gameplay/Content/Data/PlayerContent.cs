using System;
using Unity.Entities;
using UnityEngine;

namespace Ck.Gameplay
{
  [Serializable]
  public struct PlayerContent: ISharedComponentData
  {
    public GameObject PlayerPrefab;
  }
}
using System;
using Unity.Entities;
using UnityEngine;

namespace Ck.Gameplay
{
  [Serializable]
  public struct MatchResources: ISharedComponentData
  {
    public GameObject MatchPrefab;
  }
}
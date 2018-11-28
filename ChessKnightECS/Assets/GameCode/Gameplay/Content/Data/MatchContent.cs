using System;
using Unity.Entities;
using UnityEngine;

namespace Ck.Gameplay
{
  [Serializable]
  public struct MatchContent: ISharedComponentData
  {
    public GameObject MatchPrefab;
  }
}
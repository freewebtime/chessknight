﻿using System;
using Unity.Entities;
using UnityEngine;

namespace Ck.Gameplay
{
  [Serializable]
  public struct MatchDataResources: ISharedComponentData
  {
    public GameObject MatchPrefab;
  }
}
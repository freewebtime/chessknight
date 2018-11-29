using System;
using System.Collections.Generic;
using Ck.Gameplay;
using Unity.Entities;
using UnityEngine;

[Serializable]
public struct DeskResources: ISharedComponentData
{
  public GameObject DeskPrefab;
}

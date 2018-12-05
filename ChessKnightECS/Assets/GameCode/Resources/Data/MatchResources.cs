using System;
using Unity.Entities;
using UnityEngine;

namespace Ck.Resources
{
  [Serializable]
  public struct MatchResources: ISharedComponentData
  {
    public string Name;
    public int Id;

    public GameObject MatchPrefab;

    public GameObject ScenePrefab;

    public DeskResources DeskResources;
  }
}
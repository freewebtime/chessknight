using System;
using Ck.Gameplay;
using Unity.Entities;
using UnityEngine;

namespace Ck.Resources
{
  [Serializable]
  public struct DeskItemResources: ISharedComponentData
  {
    public string Name;
    public DeskItemTypes ItemType;
    public int VersionId;

    public GameObject DataPrefab;
    public GameObject ViewPrefab;
  }
}
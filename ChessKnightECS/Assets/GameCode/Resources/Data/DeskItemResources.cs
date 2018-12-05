using System;
using Unity.Entities;
using UnityEngine;

namespace Ck.Resources
{
  [Serializable]
  public struct DeskItemResources: ISharedComponentData
  {
    public string Name;
    public int GroupId;
    public int VersionId;

    public GameObject DataPrefab;
    public GameObject ViewPrefab;
  }
}
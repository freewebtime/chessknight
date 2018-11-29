using System;
using Unity.Entities;
using UnityEngine;

[Serializable]
public struct SceneResources: ISharedComponentData
{
  public GameObject[] SceneBacks;
}

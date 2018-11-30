using System;
using Unity.Entities;
using UnityEngine;

[Serializable]
public struct SceneBackDataResources: ISharedComponentData
{
  public GameObject[] SceneBacks;
}

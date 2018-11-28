using System;
using Unity.Entities;
using UnityEngine;

[Serializable]
public struct SceneBackResources: ISharedComponentData
{
  public GameObject[] SceneBacks;
}

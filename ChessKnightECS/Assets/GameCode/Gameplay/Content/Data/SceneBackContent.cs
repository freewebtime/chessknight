using System;
using Unity.Entities;
using UnityEngine;

[Serializable]
public struct SceneBackContent: ISharedComponentData
{
  public GameObject[] SceneBacks;
}

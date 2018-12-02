using System;
using Unity.Entities;
using UnityEngine;

namespace Ck.Gameplay
{
  [Serializable]
  public struct DeskMediaResources: ISharedComponentData
  {
    public GameObject[] Other;

    [Space]
    public GameObject[] Armor;
    public GameObject[] Background;
    public GameObject[] Bomb;
    public GameObject[] Figure;
    public GameObject[] Goal;
    public GameObject[] MoveTarget;

    [Space]
    public GameObject[] PlayerUnit;

    [Space]
    public GameObject[] Highlight;
  }
}
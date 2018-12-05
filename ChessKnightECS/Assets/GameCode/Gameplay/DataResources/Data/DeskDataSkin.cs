using System;
using Unity.Entities;
using UnityEngine;

namespace Ck.Gameplay
{
  [Serializable]
  public struct DeskDataSkin: ISharedComponentData
  {
    public GameObject[] Desk;

    [Space]
    public DeskItemsSkin DeskItems;

    [Space]
    public GameObject[] Armor;
    public GameObject[] Background;
    public GameObject[] Bomb;
    public GameObject[] Figure;
    public GameObject[] Goal;
    public GameObject[] MoveTarget;

    [Space]
    public GameObject[] PlayerUnit;
    public GameObject[] Highlight;
  }
}

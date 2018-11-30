using System;
using System.Collections.Generic;
using Ck.Gameplay;
using Unity.Entities;
using UnityEngine;

namespace Ck.Gameplay
{

  [Serializable]
  public struct DeskDataResources: ISharedComponentData
  {
    public GameObject DeskPrefab;

    public GameObject BackgroundLight;
    public GameObject BackgroundDark;
    public GameObject Bomb;

    public GameObject Pawn;
    public GameObject Rook;
    public GameObject Bishop;
    public GameObject Knight;
    public GameObject Queen;
    public GameObject King;

    public GameObject Goal;
    public GameObject Armor;
    public GameObject MoveTarget;
    public GameObject PlayerUnit;
  }
}

using System;
using Unity.Mathematics;
using UnityEngine;

namespace Ck.Gameplay
{
  [Serializable]
  public struct DeskItemConfig
  {
    public int2 Coordinate;
    public int DeskItemType;
    public int DeskItemVersion;
  }

}
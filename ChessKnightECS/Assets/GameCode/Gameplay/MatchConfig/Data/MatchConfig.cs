using System;
using Fwt.Core;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Ck.Gameplay
{
  [Serializable]
  public struct MatchConfig: ISharedComponentData
  {
    public DeskConfig Desk;
  }
}
using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Ck.Gameplay
{
  [CreateAssetMenu]
  public class MatchConfig: ScriptableObject
  {
    public DeskConfig Desk;
  }
}
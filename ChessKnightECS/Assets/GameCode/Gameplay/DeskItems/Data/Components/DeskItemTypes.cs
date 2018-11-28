using System;

namespace Ck.Gameplay
{
  [Serializable]
  public enum DeskItemTypes: int
  {
    Background = 0,
    Bomb = 2,
    Figure = 3,
    Lock = 4,
    MoveTarget = 5,
    Goal = 6
  }
}
using System;

namespace Ck.Gameplay
{
  [Serializable]
  public struct DeskItemSetup<TDeskItem>
  {
    public Coordinate Coordinate;
    public TDeskItem DeskItem;
  }
}
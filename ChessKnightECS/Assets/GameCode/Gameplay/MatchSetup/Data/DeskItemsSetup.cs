using System;
using Unity.Entities;

namespace Ck.Gameplay
{
  [Serializable]
  public struct DeskItemsSetup: ISharedComponentData
  {
    public DeskItemSetup<DeskItems.Background>[] Backgrounds;
    public DeskItemSetup<DeskItems.Bomb>[] Bombs;
    public DeskItemSetup<DeskItems.Figure>[] Figures;
    public DeskItemSetup<DeskItems.Goal>[] Goals;
    public DeskItemSetup<DeskItems.Lock>[] Locks;
    public DeskItemSetup<DeskItems.MoveTarget>[] MoveTargets;
  }
}

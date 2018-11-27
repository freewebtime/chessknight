using Fwt.Core;
using Unity.Entities;

namespace Ck.Gui
{

  [UpdateInGroup(typeof(GameLoop.PreRenderGroup))]
  public class GuiLoop
  {
    
    // Barriers
    [UpdateInGroup(typeof(GuiLoop))]
    [UpdateBefore(typeof(GuiInitBarrier))]
    public class GuiStartBarrier: BarrierSystem {}

    // Init
    [UpdateInGroup(typeof(GuiLoop))]
    [UpdateAfter(typeof(GuiStartBarrier))]
    [UpdateBefore(typeof(GuiCollectInputBarrier))]
    public class GuiInitBarrier: BarrierSystem {}

    // Collect Input
    [UpdateInGroup(typeof(GuiLoop))]
    [UpdateAfter(typeof(GuiInitBarrier))]
    [UpdateBefore(typeof(GuiProcessInputBarrier))]
    public class GuiCollectInputBarrier: BarrierSystem {}

    // Process Input
    [UpdateInGroup(typeof(GuiLoop))]
    [UpdateAfter(typeof(GuiCollectInputBarrier))]
    [UpdateBefore(typeof(GuiUpdateBarrier))]
    public class GuiProcessInputBarrier {}

    // Update
    [UpdateInGroup(typeof(GuiLoop))]
    [UpdateAfter(typeof(GuiProcessInputBarrier))]
    public class GuiUpdateBarrier {}


    // Groups

    // Init
    [UpdateAfter(typeof(GuiStartBarrier))]
    [UpdateBefore(typeof(GuiInitBarrier))]
    public class InitGroup {}

    // Collect Input
    [UpdateAfter(typeof(GuiInitBarrier))]
    [UpdateBefore(typeof(GuiCollectInputBarrier))]
    public class CollectInputGroup {}

    // Process Input
    [UpdateAfter(typeof(GuiCollectInputBarrier))]
    [UpdateBefore(typeof(GuiProcessInputBarrier))]
    public class ProcessInputGroup {}

    // Update
    [UpdateAfter(typeof(GuiProcessInputBarrier))]
    [UpdateBefore(typeof(GuiUpdateBarrier))]
    public class UpdateGroup 
    {

      // Barriers
      
      // Start update screens
      // [UpdateInGroup(typeof(UpdateGroup))]
      [UpdateAfter(typeof(GuiProcessInputBarrier))]
      [UpdateBefore(typeof(GuiHideBarrier))]
      public class GuiUpdateStartBarrier: BarrierSystem {}

      // Hide screens
      // [UpdateInGroup(typeof(UpdateGroup))]
      [UpdateAfter(typeof(GuiUpdateStartBarrier))]
      [UpdateBefore(typeof(GuiShowBarrier))]
      public class GuiHideBarrier: BarrierSystem {}

      // Show screens
      // [UpdateInGroup(typeof(UpdateGroup))]
      [UpdateAfter(typeof(GuiHideBarrier))]
      [UpdateBefore(typeof(GuiUpdateBarrier))]
      public class GuiShowBarrier: BarrierSystem {}


      // Groups

      // Hide screens
      [UpdateAfter(typeof(GuiStartBarrier))]
      [UpdateBefore(typeof(GuiHideBarrier))]
      public class HideGroup {}

      // Show screens
      [UpdateAfter(typeof(GuiHideBarrier))]
      [UpdateBefore(typeof(GuiShowBarrier))]
      public class ShowGroup {}
    }

  }

}

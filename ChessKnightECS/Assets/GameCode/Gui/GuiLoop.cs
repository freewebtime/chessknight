using Fwt.Core;
using Unity.Entities;

namespace Ck.Gui
{

  [UpdateInGroup(typeof(GameLoop.PreRenderGroup))]
  public class GuiLoop
  {
    
    // Barriers
    [UpdateInGroup(typeof(GuiLoop))]
    public class GuiStartBarrier: BarrierSystem {}

    // Init
    [UpdateInGroup(typeof(GuiLoop))]
    public class GuiInitBarrier: BarrierSystem {}

    // Collect Input
    [UpdateInGroup(typeof(GuiLoop))]
    public class GuiCollectInputBarrier: BarrierSystem {}

    // Process Input
    [UpdateInGroup(typeof(GuiLoop))]
    public class GuiProcessInputBarrier {}

    // Update
    [UpdateInGroup(typeof(GuiLoop))]
    public class GuiUpdateBarrier {}


    // Groups

    // Init
    [UpdateInGroup(typeof(GuiLoop))]
    [UpdateAfter(typeof(GuiStartBarrier))]
    [UpdateBefore(typeof(GuiInitBarrier))]
    public class InitGroup {}

    // Collect Input
    [UpdateInGroup(typeof(GuiLoop))]
    [UpdateAfter(typeof(GuiInitBarrier))]
    [UpdateBefore(typeof(GuiCollectInputBarrier))]
    public class CollectInputGroup {}

    // Process Input
    [UpdateInGroup(typeof(GuiLoop))]
    [UpdateAfter(typeof(GuiCollectInputBarrier))]
    [UpdateBefore(typeof(GuiProcessInputBarrier))]
    public class ProcessInputGroup {}

    // Update
    [UpdateInGroup(typeof(GuiLoop))]
    [UpdateAfter(typeof(GuiProcessInputBarrier))]
    [UpdateBefore(typeof(GuiUpdateBarrier))]
    public class UpdateGroup 
    {

      // Barriers
      
      // Start update screens
      [UpdateInGroup(typeof(UpdateGroup))]
      [UpdateAfter(typeof(GuiProcessInputBarrier))]
      [UpdateBefore(typeof(GuiHideBarrier))]
      public class GuiUpdateStartBarrier: BarrierSystem {}

      // Hide screens
      [UpdateInGroup(typeof(UpdateGroup))]
      [UpdateAfter(typeof(GuiUpdateStartBarrier))]
      [UpdateBefore(typeof(GuiShowBarrier))]
      public class GuiHideBarrier: BarrierSystem {}

      // Show screens
      [UpdateInGroup(typeof(UpdateGroup))]
      [UpdateAfter(typeof(GuiHideBarrier))]
      [UpdateBefore(typeof(GuiUpdateBarrier))]
      public class GuiShowBarrier: BarrierSystem {}


      // Groups

      // Hide screens
      [UpdateInGroup(typeof(GuiLoop))]
      [UpdateAfter(typeof(GuiStartBarrier))]
      [UpdateBefore(typeof(GuiHideBarrier))]
      public class HideGroup {}

      // Show screens
      [UpdateInGroup(typeof(GuiLoop))]
      [UpdateAfter(typeof(GuiHideBarrier))]
      [UpdateBefore(typeof(GuiShowBarrier))]
      public class ShowGroup {}
    }

  }

}

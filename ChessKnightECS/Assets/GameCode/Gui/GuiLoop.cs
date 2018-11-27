using Fwt.Core;
using Unity.Entities;

namespace Ck.Gui
{

  [UpdateInGroup(typeof(GameLoop.PreRenderGroup))]
  public class GuiLoop
  {
    
    // Init
    [UpdateInGroup(typeof(GuiLoop))]
    [UpdateBefore(typeof(InitBarrier))]
    public class InitGroup {}

    [UpdateInGroup(typeof(GuiLoop))]
    [UpdateBefore(typeof(CollectInputGroup))]
    public class InitBarrier: BarrierSystem {}

    // Collect Input
    [UpdateInGroup(typeof(GuiLoop))]
    [UpdateBefore(typeof(CollectInputBarrier))]
    public class CollectInputGroup {}

    [UpdateInGroup(typeof(GuiLoop))]
    [UpdateAfter(typeof(InitBarrier))]
    [UpdateBefore(typeof(ProcessInputGroup))]
    public class CollectInputBarrier: BarrierSystem {}

    // Process Input
    [UpdateInGroup(typeof(GuiLoop))]
    [UpdateBefore(typeof(ProcessInputBarrier))]
    public class ProcessInputGroup {}

    [UpdateInGroup(typeof(GuiLoop))]
    [UpdateAfter(typeof(CollectInputBarrier))]
    [UpdateBefore(typeof(UpdateGroup))]
    public class ProcessInputBarrier {}

    // Update
    [UpdateInGroup(typeof(GuiLoop))]
    [UpdateBefore(typeof(UpdateBarrier))]
    public class UpdateGroup 
    {

      // Hide screens
      [UpdateInGroup(typeof(UpdateGroup))]
      [UpdateBefore(typeof(ShowGroup))]
      public class HideGroup {}

      // Show screens
      [UpdateInGroup(typeof(UpdateGroup))]
      public class ShowGroup {}

    }

    [UpdateInGroup(typeof(GuiLoop))]
    [UpdateAfter(typeof(ProcessInputBarrier))]
    public class UpdateBarrier {}

  }

}

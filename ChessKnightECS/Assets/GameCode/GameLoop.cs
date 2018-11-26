using Unity.Entities;

[UpdateAfter(typeof(UnityEngine.Experimental.PlayerLoop.Update))]
public static class GameLoop 
{

  // Initialize
  [UpdateInGroup(typeof(GameLoop))]
  [UpdateBefore(typeof(InitializeBarrier))]
  public class InitializeGroup {}

  [UpdateInGroup(typeof(GameLoop))]
  [UpdateBefore(typeof(CollectInputGroup))]
  public class InitializeBarrier: BarrierSystem {}

  // Collect Input
  [UpdateInGroup(typeof(GameLoop))]
  [UpdateBefore(typeof(CollectInputBarrier))]
  public class CollectInputGroup {}

  [UpdateInGroup(typeof(GameLoop))]
  [UpdateBefore(typeof(ProcessInputGroup))]
  public class CollectInputBarrier: BarrierSystem {}

  // Process Input
  [UpdateInGroup(typeof(GameLoop))]
  [UpdateBefore(typeof(ProcessInputBarrier))]
  public class ProcessInputGroup {}

  [UpdateInGroup(typeof(GameLoop))]
  [UpdateBefore(typeof(PrepareUpdateGroup))]
  public class ProcessInputBarrier: BarrierSystem {}

  // Prepare Update
  [UpdateInGroup(typeof(GameLoop))]
  [UpdateBefore(typeof(PrepareUpdateBarrier))]
  public class PrepareUpdateGroup {}

  [UpdateInGroup(typeof(GameLoop))]
  [UpdateBefore(typeof(UpdateGroup))]
  public class PrepareUpdateBarrier: BarrierSystem {}

  // Update
  [UpdateInGroup(typeof(GameLoop))]
  [UpdateBefore(typeof(UpdateBarrier))]
  public class UpdateGroup {}

  [UpdateInGroup(typeof(GameLoop))]
  [UpdateBefore(typeof(PostUpdateGroup))]
  public class UpdateBarrier: BarrierSystem {}

  // Post Update
  [UpdateInGroup(typeof(GameLoop))]
  [UpdateBefore(typeof(PostUpdateBarrier))]
  public class PostUpdateGroup {}

  [UpdateInGroup(typeof(GameLoop))]
  [UpdateBefore(typeof(PreRenderGroup))]
  public class PostUpdateBarrier: BarrierSystem {}

  // PreRender
  [UpdateInGroup(typeof(GameLoop))]
  [UpdateBefore(typeof(PreRenderBarrier))]
  public class PreRenderGroup {}

  [UpdateInGroup(typeof(GameLoop))]
  [UpdateBefore(typeof(RenderGroup))]
  public class PreRenderBarrier: BarrierSystem {}

  // Render
  [UpdateInGroup(typeof(GameLoop))]
  [UpdateBefore(typeof(RenderBarrier))]
  public class RenderGroup {}

  [UpdateInGroup(typeof(GameLoop))]
  [UpdateBefore(typeof(CleanupGroup))]
  public class RenderBarrier: BarrierSystem {}

  // Cleanup
  [UpdateInGroup(typeof(GameLoop))]
  [UpdateBefore(typeof(CleanupBarrier))]
  public class CleanupGroup {}

  [UpdateInGroup(typeof(GameLoop))]
  public class CleanupBarrier: BarrierSystem {}

}
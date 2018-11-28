using Fwt.Core.Collections;
using Unity.Collections;
using Unity.Entities;

namespace Ck.Gameplay
{
  
  [UpdateBefore(typeof(SafeArraySystem<DeskLayer<DeskLayers.Background>>))]
  public class InitDeskLayerSystemBackground: InitDeskLayerSystem<DeskLayers.Background> {}
  
  [UpdateBefore(typeof(SafeArraySystem<DeskLayer<DeskLayers.Bomb>>))]
  public class InitDeskLayerSystemBomb: InitDeskLayerSystem<DeskLayers.Bomb> {}
  
  [UpdateBefore(typeof(SafeArraySystem<DeskLayer<DeskLayers.Figure>>))]
  public class InitDeskLayerSystemFigure: InitDeskLayerSystem<DeskLayers.Figure> {}
  
  [UpdateBefore(typeof(SafeArraySystem<DeskLayer<DeskLayers.Goal>>))]
  public class InitDeskLayerSystemGoal: InitDeskLayerSystem<DeskLayers.Goal> {}
  
  [UpdateBefore(typeof(SafeArraySystem<DeskLayer<DeskLayers.Lock>>))]
  public class InitDeskLayerSystemLock: InitDeskLayerSystem<DeskLayers.Lock> {}
  
  [UpdateBefore(typeof(SafeArraySystem<DeskLayer<DeskLayers.MoveTarget>>))]
  public class InitDeskLayerSystemMoveTarget: InitDeskLayerSystem<DeskLayers.MoveTarget> {}


  public abstract class InitDeskLayerSystem<TLayer> : ComponentSystem where TLayer: struct
  {
    protected struct InitDeskLayerCache: ISystemStateComponentData {}

    protected struct Added
    {
      public readonly int Length;
      [ReadOnly] public EntityArray Entity;
      [ReadOnly] public ComponentDataArray<Desk> Desk;
      [ReadOnly] public ComponentDataArray<DeskSize> DeskSize;
      [ReadOnly] public SharedComponentDataArray<SafeArray<DeskLayer<TLayer>>> Layer;

      public SubtractiveComponent<InitDeskLayerCache> NoCache;
    }

    protected struct Removed
    {
      public readonly int Length;
      [ReadOnly] public EntityArray Entity;
      [ReadOnly] public ComponentDataArray<InitDeskLayerCache> Cache;

      public SubtractiveComponent<Desk> NoDesk;
      public SubtractiveComponent<SafeArray<DeskLayer<TLayer>>> NoLayer;
    }

    [Inject] protected Added added;
    [Inject] protected Removed removed;

    protected override void OnUpdate()
    {
      for (int i = 0; i < added.Length; i++)
      {
        var deskSize = added.DeskSize[i].Value;
        var cellsCount = deskSize.x * deskSize.y;

        var layerData = added.Layer[i];
        layerData.Length = cellsCount;

        var entity = added.Entity[i];
        PostUpdateCommands.SetSharedComponent(entity, layerData);
        PostUpdateCommands.AddComponent(entity, new InitDeskLayerCache());
      }

      for (int i = 0; i < removed.Length; i++)
      {
        PostUpdateCommands.RemoveComponent<InitDeskLayerCache>(removed.Entity[i]);
      }
    }
  }

}
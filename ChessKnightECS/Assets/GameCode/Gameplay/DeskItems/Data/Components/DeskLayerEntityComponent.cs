using Unity.Entities;

namespace Ck.Gameplay
{

  public abstract class DeskLayerEntityComponent<TLayer>: ComponentDataWrapper<DeskLayerEntity<TLayer>> where TLayer: struct {}
}
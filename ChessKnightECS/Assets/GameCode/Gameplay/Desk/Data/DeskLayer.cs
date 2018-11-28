using Unity.Entities;

namespace Ck.Gameplay
{
  public struct DeskLayer<TLayer>: ISharedComponentData where TLayer: struct
  {
    public TLayer Value;
  }

}
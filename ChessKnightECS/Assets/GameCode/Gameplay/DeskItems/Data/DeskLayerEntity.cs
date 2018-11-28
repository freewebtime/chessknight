using System;
using Unity.Entities;

namespace Ck.Gameplay
{
  [Serializable]
  public struct DeskLayerEntity<TLayer>: IComponentData where TLayer: struct {}
}
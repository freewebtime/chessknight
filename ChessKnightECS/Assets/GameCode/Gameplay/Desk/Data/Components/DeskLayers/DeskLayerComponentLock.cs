using Fwt.Core.Collections;
using Unity.Entities;

namespace Ck.Gameplay
{
  public class DeskLayerComponentLock: SharedComponentDataWrapper<SafeArray<DeskLayer<DeskLayers.Lock>>> {}

}
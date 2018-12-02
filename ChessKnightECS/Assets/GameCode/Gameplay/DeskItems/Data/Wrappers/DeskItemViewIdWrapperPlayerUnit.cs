using UnityEngine;

namespace Ck.Gameplay
{
  public class DeskItemViewIdWrapperPlayerUnit: DeskItemViewIdWrapper
  {
    [Header("Change this value to refresh ViewId by selection")]
    public ChessFigureTypes ViewType;

    private void OnValidate() {
      var val = Value;
      val.Id = (int)ViewType;
      Value = val;
    }
  }
}
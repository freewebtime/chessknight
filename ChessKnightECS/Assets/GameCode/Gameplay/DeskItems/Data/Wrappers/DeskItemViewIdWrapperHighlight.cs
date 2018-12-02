using UnityEngine;

namespace Ck.Gameplay
{
  public class DeskItemViewIdWrapperHighlight: DeskItemViewIdWrapper
  {
    [Header("Change this value to refresh ViewId by selection")]
    public HighlightTypes ViewType;

    private void OnValidate() {
      var val = Value;
      val.Id = (int)ViewType;
      Value = val;
    }
  }
}
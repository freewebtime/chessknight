using Ck.Gameplay;
using UnityEngine;

namespace Ck.Resources
{
  [CreateAssetMenu]
  public class DeskItemResourcesWrapperHighlight: DeskItemResourcesWrapper
  {
    public HighlightTypes HighlightType;

    public override void Init()
    {
      var value = Value;
      value.VersionId = (int)HighlightType;
      Value = value;
    }
  }
}
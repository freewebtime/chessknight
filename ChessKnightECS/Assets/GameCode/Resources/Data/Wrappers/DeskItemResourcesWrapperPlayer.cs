using Ck.Gameplay;
using UnityEngine;

namespace Ck.Resources
{
  [CreateAssetMenu]
  public class DeskItemResourcesWrapperPlayer: DeskItemResourcesWrapper
  {
    public ChessFigureTypes FigureType;

    public override void Init()
    {
      var value = Value;
      value.VersionId = (int)FigureType;
      Value = value;
    }
  }
}
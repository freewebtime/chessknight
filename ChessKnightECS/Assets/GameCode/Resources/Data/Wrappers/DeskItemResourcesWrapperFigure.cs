using Ck.Gameplay;
using UnityEngine;

namespace Ck.Resources
{
  [CreateAssetMenu]
  public class DeskItemResourcesWrapperFigure: DeskItemResourcesWrapper
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
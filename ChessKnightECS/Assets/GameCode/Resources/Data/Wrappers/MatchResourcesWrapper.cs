using Fwt.Core;
using UnityEngine;

namespace Ck.Resources
{
  [CreateAssetMenu]
  public class MatchResourcesWrapper: ScriptableObjectWrapper<MatchResources> 
  {
    public DeskResourcesWrapper Desk;

    private void OnValidate() {
      Init();
    }

    public void Init()
    {
      if (Desk == null) {
        Value = default;
        return;
      }

      Desk.Init();

      var value = Value;
      value.DeskResources = Desk.Value;
      Value = value;
    }
  }
}
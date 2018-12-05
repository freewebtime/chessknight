using Fwt.Core;
using UnityEngine;

namespace Ck.Resources
{
  [CreateAssetMenu]
  public class DeskResourcesWrapper: ScriptableObjectWrapper<DeskResources> 
  {
    public DeskItemsResourcesWrapper DeskItems;

    private void OnValidate() {
      Init();
    }

    public void Init()
    {
      if (DeskItems == null) {
        Value = default;
        return;
      }

      DeskItems.Init();

      var value = Value;
      value.DeskItems = DeskItems.Value;
      Value = value;
    }
  }
}
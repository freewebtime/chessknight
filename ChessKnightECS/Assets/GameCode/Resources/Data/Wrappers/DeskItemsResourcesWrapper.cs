using Fwt.Core;
using UnityEngine;

namespace Ck.Resources
{
  [CreateAssetMenu]
  public class DeskItemsResourcesWrapper: ScriptableObjectWrapper<DeskItemsResources> 
  {
    public DeskItemsGroupResourcesWrapper[] DeskItemsGroups;

    private void OnValidate() {
      Init();
    }

    public void Init()
    {
      var deskItemsGroups = new DeskItemsGroupResources[DeskItemsGroups.Length];
      for (int i = 0; i < deskItemsGroups.Length; i++)
      {
        var deskItemGroupWrapper = DeskItemsGroups[i];
        if (deskItemGroupWrapper == null) {
          continue;
        }

        deskItemGroupWrapper.Init();

        deskItemsGroups[i] = deskItemGroupWrapper.Value;
      }

      var value = Value;
      value.DeskItemsGroups = deskItemsGroups;
      Value = value;
    }
  }
}
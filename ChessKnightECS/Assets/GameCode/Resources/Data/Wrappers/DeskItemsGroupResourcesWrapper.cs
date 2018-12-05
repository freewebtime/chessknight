using System;
using Fwt.Core;
using UnityEngine;

namespace Ck.Resources
{
  [CreateAssetMenu]
  public class DeskItemsGroupResourcesWrapper: ScriptableObjectWrapper<DeskItemsGroupResources> 
  {
    public bool IsAutoSetItemVersionId;
    public int GroupId;
    public DeskItemResourcesWrapper[] DeskItems;

    private void OnValidate() {
      Init();
    }

    public void Init()
    {
      var deskItems = new DeskItemResources[DeskItems.Length];
      var deskItemsIds = new int[deskItems.Length];
      for (int i = 0; i < deskItems.Length; i++)
      {
        var deskItemWrapper = DeskItems[i];
        var deskItem = deskItems[i];
        
        if (deskItemWrapper != null) {
          deskItemWrapper.Init();

          deskItem = deskItemWrapper.Value;
          deskItem.Name = deskItemWrapper.name;
        }

        deskItem.GroupId = GroupId;

        if (IsAutoSetItemVersionId) {
          deskItem.VersionId = i;
        }

        deskItemsIds[i] = deskItem.VersionId;

        deskItems[i] = deskItem;
      }
      Array.Sort(deskItemsIds, deskItems);

      var value = Value;
      value.DeskItems = deskItems;
      Value = value;
    }
  }
}
using System;
using System.Collections.Generic;
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
      var deskItemsByItemType = new Dictionary<int, DeskItemsGroupResources>();
      var deskItemTypes = new int[deskItemsGroups.Length];

      for (int i = 0; i < deskItemsGroups.Length; i++)
      {
        var deskItemGroup = deskItemsGroups[i];

        var deskItemGroupWrapper = DeskItemsGroups[i];
        if (deskItemGroupWrapper != null) {
          deskItemGroupWrapper.Init();
          deskItemGroup = deskItemGroupWrapper.Value;
          deskItemGroup.Name = deskItemGroupWrapper.name;
          deskItemGroup.ItemsType = deskItemGroupWrapper.ItemsType;
        }

        deskItemsByItemType[(int)deskItemGroup.ItemsType] = deskItemGroup;

        deskItemsGroups[i] = deskItemGroup;
        deskItemTypes[i] = (int)deskItemGroup.ItemsType;
      }
      Array.Sort(deskItemTypes, deskItemsGroups);

      var value = Value;
      value.DeskItemsGroups = deskItemsGroups;
      value.DeskItemsByItemType = deskItemsByItemType;
      Value = value;
    }
  }
}
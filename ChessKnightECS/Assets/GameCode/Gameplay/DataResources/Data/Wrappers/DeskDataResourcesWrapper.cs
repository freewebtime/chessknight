using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Ck.Gameplay
{
  public class DeskDataResourcesWrapper: SharedComponentDataWrapper<DeskDataResources> 
  {
    public DeskDataSkinWrapper DefaultDeskPrefab;

    private void OnValidate() {
      if (DefaultDeskPrefab) {
        var val = Value;
        val.DefaultDesk = DefaultDeskPrefab.Value;

        Value = val;
      }
    }
  }
}

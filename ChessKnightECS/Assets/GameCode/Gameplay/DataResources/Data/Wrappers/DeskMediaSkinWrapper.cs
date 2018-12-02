using System;
using Fwt.Core;
using UnityEngine;

namespace Ck.Gameplay
{
  [CreateAssetMenu]
  public class DeskMediaSkinWrapper: ScriptableObjectWrapper<DeskMediaSkin> 
  {
    private void OnValidate() {
      var deskSkin = Value;
      
      // sort armors
      SortDeskItemViewsByViewId(deskSkin.Armor);

      // sort backgrounds
      SortDeskItemViewsByViewId(deskSkin.Background);

      // sort bombs
      SortDeskItemViewsByViewId(deskSkin.Bomb);

      // sort figures
      SortDeskItemViewsByViewId(deskSkin.Figure);
      
      // sort goals
      SortDeskItemViewsByViewId(deskSkin.Goal);

      // sort move targets
      SortDeskItemViewsByViewId(deskSkin.MoveTarget);

      // sort player units
      SortDeskItemViewsByViewId(deskSkin.PlayerUnit);

      // sort highlights
      SortDeskItemViewsByViewId(deskSkin.Highlight);
    }

    public void SortDeskItemViewsByViewId(GameObject[] views) 
    {
      if (views == null) {
        return;
      }

      var keys = new int[views.Length];
      for (int i = 0; i < keys.Length; i++)
      {
        var viewId = int.MaxValue;
        var viewGo = views[i];
  
        if (viewGo != null) {
          var viewIdWrapper = viewGo.GetComponent<DeskItemViewIdWrapper>();
          if (viewIdWrapper != null) {
            viewId = viewIdWrapper.Value.Id;
          }
        }


        keys[i] = viewId;
      }

      Array.Sort(keys, views);
    }
  }
}

using Fwt.Core;
using UnityEngine;

namespace Ck.Gameplay
{
  [CreateAssetMenu]
  public class DeskItemsSkinWrapper: ScriptableObjectWrapper<DeskItemsSkin>
  {
    private void OnValidate() {
      for (int i = 0; i < Value.DeskItems.Length; i++)
      {
        var deskItem = Value.DeskItems[i];
        deskItem.GroupId = i;
        Value.DeskItems[i] = deskItem;
      }
    }
  }

}

using Fwt.Core;
using UnityEngine;

namespace Ck.Resources
{
  [CreateAssetMenu]
  public class DeskItemResourcesWrapper: ScriptableObjectWrapper<DeskItemResources> 
  {
    private void OnValidate() {
      Init();
    }

    public virtual void Init()
    {

    }
  }
}
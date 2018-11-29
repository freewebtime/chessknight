using Unity.Entities;
using UnityEngine;

namespace Ck.Gameplay
{
  public struct GamestateResources: ISharedComponentData 
  {
    public GameObject GamestatePrefab;
  }

}
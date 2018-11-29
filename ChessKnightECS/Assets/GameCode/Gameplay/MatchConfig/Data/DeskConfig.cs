using Unity.Mathematics;
using UnityEngine;

namespace Ck.Gameplay
{
  [CreateAssetMenu]
  public class DeskConfig: ScriptableObject
  {
    public int2 Size;

    public DeskItemConfig[] DeskItems;
  }

}
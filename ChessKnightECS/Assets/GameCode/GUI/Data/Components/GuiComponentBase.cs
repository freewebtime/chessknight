using Unity.Entities;
using UnityEngine;

namespace Ck.Data
{
  public class GuiComponentBase: MonoBehaviour
  {
    public GameApi GameApi
    {
      get
      {
        return World.Active.GetExistingManager<GameApi>();        
      }
    }
  }
}
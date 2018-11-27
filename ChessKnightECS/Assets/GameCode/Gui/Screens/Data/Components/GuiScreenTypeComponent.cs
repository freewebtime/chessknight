using Unity.Entities;

namespace Ck.Gui
{
  public abstract class GuiScreenTypeComponent<TScreen>: ComponentDataWrapper<GuiScreenType<TScreen>> where TScreen: struct 
  {
    protected EntityManager EntityManager
    {
      get {
        return World.Active.GetOrCreateManager<EntityManager>();
      }
    }

    protected World World
    {
      get
      {
        return World.Active;
      }
    }

    protected GameApi GameApi
    {
      get
      {
        return World.GetExistingManager<GameApi>();
      }
    }
  }
}
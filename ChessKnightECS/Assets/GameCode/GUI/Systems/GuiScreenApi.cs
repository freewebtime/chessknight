using Ck.Data;
using Fwt.Core;
using Fwt.Core.Gui;
using Unity.Collections;
using Unity.Entities;

namespace Ck.Logic
{
  [UpdateAfter(typeof(GuiWidgetsApi))]
  [UpdateBefore(typeof(HideAllGuiScreensSystem))]
  public class GuiScreenApi : ComponentSystem
  {
    [Inject] EndFrameBarrier endFrameBarrier;

    EntityArchetype showAllScreensArchetype;
    EntityArchetype hideAllScreensArchetype;

    protected override void OnCreateManager()
    {
      showAllScreensArchetype = EntityManager.CreateArchetype(
        ComponentType.Create<Command>(),
        ComponentType.Create<ShowAllGuiScreensCommand>()
      );
      hideAllScreensArchetype = EntityManager.CreateArchetype(
        ComponentType.Create<Command>(),
        ComponentType.Create<HideAllGuiScreensCommand>()
      );
    }

    protected override void OnDestroyManager()
    {
    }

    public void ShowAllScreens()
    {
      PostUpdateCommands.CreateEntity(showAllScreensArchetype);
    }
    public void ShowAllScreensNow()
    {
      EntityManager.CreateEntity(showAllScreensArchetype);
    }

    public void HideAllScreens()
    {
      PostUpdateCommands.CreateEntity(hideAllScreensArchetype);
    }
    public void HideAllScreensNow()
    {
      EntityManager.CreateEntity(hideAllScreensArchetype);
    }

    protected override void OnUpdate()
    {
    }
  }
}
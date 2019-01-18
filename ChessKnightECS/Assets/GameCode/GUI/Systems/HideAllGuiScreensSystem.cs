using Ck.Data;
using Fwt.Core.Data;
using Fwt.Core.Gui;
using Unity.Collections;
using Unity.Entities;

namespace Ck.Logic
{
  [UpdateBefore(typeof(ShowHideWidgetSystem))]
  [UpdateBefore(typeof(ShowAllGuiScreensSystem))]
  [UpdateAfter(typeof(GuiWidgetsApi))]
  public class HideAllGuiScreensSystem : ComponentSystem
  {
    struct RequestGroup
    {
      public readonly int Length;
      [ReadOnly] public ComponentDataArray<HideAllGuiScreensCommand> Command;
    }

    [Inject] RequestGroup requestGroup;

    protected override void OnUpdate()
    {
      var screensGroup = GetComponentGroup(
        ComponentType.Create<GuiScreen>(),
        ComponentType.Create<Visible>()
      );
      
      var screenEntities = screensGroup.GetEntityArray();
      for (int i = 0; i < screenEntities.Length; i++)
      {
        var screenEntity = screenEntities[i];
        PostUpdateCommands.RemoveComponent<Visible>(screenEntity);
      }
    }
  }
}
using Ck.Data;
using Fwt.Core.Data;
using Fwt.Core.Gui;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Ck.Logic
{
  [UpdateBefore(typeof(ShowHideWidgetSystem))]
  [UpdateBefore(typeof(GuiWidgetsApi))]
  [UpdateBefore(typeof(ShowAllGuiScreensSystem))]
  [UpdateAfter(typeof(GuiScreenBarrier))]
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
      if (requestGroup.Length == 0) {
        return;
      }

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
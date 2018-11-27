using Fwt.Core.Data;
using Unity.Collections;
using Unity.Entities;

namespace Ck.Gui
{
  [UpdateInGroup(typeof(GuiLoop.UpdateGroup.HideGroup))]
  public abstract class HideAllGuiScreensSystem: ComponentSystem
  {
    protected struct WidgetsGroup
    {
      public readonly int Length;
      [ReadOnly] public EntityArray Entity;
      [ReadOnly] public ComponentDataArray<GuiScreen> Screen;
      [ReadOnly] public ComponentDataArray<Visible> Visible;
    }

    protected struct CommandGroup
    {
      public readonly int Length;
      [ReadOnly] public ComponentDataArray<HideAllGuiScreensCommand> Command;
    }

    [Inject] CommandGroup commandGroup;
    [Inject] WidgetsGroup widgetsGroup;

    protected override void OnUpdate()
    {
      if (commandGroup.Length == 0) {
        return;
      }

      for (int i = 0; i < widgetsGroup.Length; i++)
      {
        PostUpdateCommands.RemoveComponent<Visible>(widgetsGroup.Entity[i]);
      }
    }
  }

}
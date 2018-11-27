using Fwt.Core.Data;
using Unity.Collections;
using Unity.Entities;

namespace Ck.Gui
{

  // Main Menu
  [UpdateInGroup(typeof(GuiLoop.UpdateGroup.HideGroup))]
  public class HideGuiScreenSystemMainMenu: HideGuiScreenSystem<MainMenuScreen> {}

  // Game Pause
  [UpdateInGroup(typeof(GuiLoop.UpdateGroup.HideGroup))]
  public class HideGuiScreenSystemGamePause: HideGuiScreenSystem<GamePauseScreen> {}

  // Match
  [UpdateInGroup(typeof(GuiLoop.UpdateGroup.HideGroup))]
  public class HideGuiScreenSystemMatch: HideGuiScreenSystem<MatchScreen> {}

  // You Win
  [UpdateInGroup(typeof(GuiLoop.UpdateGroup.HideGroup))]
  public class HideGuiScreenSystemYouWin: HideGuiScreenSystem<YouWinScreen> {}

  // You Lose
  [UpdateInGroup(typeof(GuiLoop.UpdateGroup.HideGroup))]
  public class HideGuiScreenSystemYouLose: HideGuiScreenSystem<YouLoseScreen> {}

  // base class
  [UpdateInGroup(typeof(GuiLoop.UpdateGroup.HideGroup))]
  public abstract class HideGuiScreenSystem<TScreen>: ComponentSystem where TScreen: struct
  {
    protected struct WidgetsGroup
    {
      public readonly int Length;
      [ReadOnly] public EntityArray Entity;
      [ReadOnly] public ComponentDataArray<GuiScreenType<TScreen>> Screen;
      [ReadOnly] public ComponentDataArray<Visible> Visible;
    }

    protected struct CommandGroup
    {
      public readonly int Length;
      [ReadOnly] public ComponentDataArray<HideGuiScreenCommand<TScreen>> Command;
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
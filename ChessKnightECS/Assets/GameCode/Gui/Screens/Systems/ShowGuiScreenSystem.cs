using Fwt.Core.Data;
using Unity.Collections;
using Unity.Entities;

namespace Ck.Gui
{

  // Main Menu
  [UpdateInGroup(typeof(GuiLoop.UpdateGroup.HideGroup))]
  public class ShowGuiScreenSystemMainMenu: ShowGuiScreenSystem<MainMenuScreen> {}

  // Game Pause
  [UpdateInGroup(typeof(GuiLoop.UpdateGroup.ShowGroup))]
  public class ShowGuiScreenSystemGamePause: ShowGuiScreenSystem<GamePauseScreen> {}

  // Match
  [UpdateInGroup(typeof(GuiLoop.UpdateGroup.ShowGroup))]
  public class ShowGuiScreenSystemMatch: ShowGuiScreenSystem<MatchScreen> {}

  // You Win
  [UpdateInGroup(typeof(GuiLoop.UpdateGroup.ShowGroup))]
  public class ShowGuiScreenSystemYouWin: ShowGuiScreenSystem<YouWinScreen> {}

  // You Lose
  [UpdateInGroup(typeof(GuiLoop.UpdateGroup.ShowGroup))]
  public class ShowGuiScreenSystemYouLose: ShowGuiScreenSystem<YouLoseScreen> {}

  // base class
  [UpdateInGroup(typeof(GuiLoop.UpdateGroup.ShowGroup))]
  public abstract class ShowGuiScreenSystem<TScreen>: ComponentSystem where TScreen: struct
  {
    protected struct WidgetsGroup
    {
      public readonly int Length;
      [ReadOnly] public EntityArray Entity;
      [ReadOnly] public ComponentDataArray<GuiScreenType<TScreen>> Screen;
      public SubtractiveComponent<Visible> NotVisible;
    }

    protected struct CommandGroup
    {
      public readonly int Length;
      [ReadOnly] public ComponentDataArray<ShowGuiScreenCommand<TScreen>> Command;
    }

    [Inject] protected CommandGroup commandGroup;
    [Inject] protected WidgetsGroup widgetsGroup;

    protected override void OnUpdate()
    {
      if (commandGroup.Length == 0) {
        return;
      }

      for (int i = 0; i < widgetsGroup.Length; i++)
      {
        PostUpdateCommands.AddComponent(widgetsGroup.Entity[i], new Visible());
      }
    }
  }

}
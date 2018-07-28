using Assets.GameCode.Shared;
using Assets.GameCode.UiSystem.Data.Screens;
using Assets.GameCode.UiSystem.Logic;
using UnityEngine;

namespace Assets.GameCode.UiSystem.Components.Controllers
{
    [RequireComponent(typeof(UiScreenCheatboardComponent))]
    public class CheatboardController: MonoBehaviour
    {
        public void OnCloseClicked()
        {
            UiScreenApi.SetScreenVisibility(UiScreenType.Cheatboard, Booleans.False);
        }
        public void OnWinLevelClicked()
        {
            UiScreenApi.SetAllScreensVisibility(Booleans.False);
            UiScreenApi.SetScreenVisibility(UiScreenType.MainMenu, Booleans.True);
        }
        public void OnLoseLevelClicked()
        {
            UiScreenApi.SetAllScreensVisibility(Booleans.False);
            UiScreenApi.SetScreenVisibility(UiScreenType.MainMenu, Booleans.True);
        }
    }
}

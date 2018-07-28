using Assets.GameCode.Shared;
using Assets.GameCode.UiSystem.Data.Screens;
using Assets.GameCode.UiSystem.Logic;
using UnityEngine;

namespace Assets.GameCode.UiSystem.Components.Controllers
{
    [RequireComponent(typeof(UiScreenMainMenuComponent))]
    public class MainMenuController: MonoBehaviour
    {
        public void OnPlayClicked()
        {
            UiScreenApi.SetAllScreensVisibility(Booleans.False);
            UiScreenApi.SetScreenVisibility(UiScreenType.Levelboard, Booleans.True);
        }

        public void OnSettingsClicked()
        {
            UiScreenApi.SetAllScreensVisibility(Booleans.False);
            UiScreenApi.SetScreenVisibility(UiScreenType.Settings, Booleans.True);
        }

        public void OnCreditsClicked()
        {
            UiScreenApi.SetAllScreensVisibility(Booleans.False);
            UiScreenApi.SetScreenVisibility(UiScreenType.Credits, Booleans.True);
        }

        public void OnExitClicked()
        {
            Application.Quit();
        }
    }
}

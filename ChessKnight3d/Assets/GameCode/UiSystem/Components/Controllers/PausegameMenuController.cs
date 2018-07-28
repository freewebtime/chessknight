using Assets.GameCode.Shared;
using Assets.GameCode.UiSystem.Data.Screens;
using Assets.GameCode.UiSystem.Logic;
using UnityEngine;

namespace Assets.GameCode.UiSystem.Components.Controllers
{
    [RequireComponent(typeof(UiScreenPausegameComponent))]
    public class PausegameMenuController : MonoBehaviour
    {
        public void OnMainMenuClicked()
        {
            UiScreenApi.SetAllScreensVisibility(Booleans.False);
            UiScreenApi.SetScreenVisibility(UiScreenType.MainMenu, Booleans.True);
        }
        public void OnReplayClicked()
        {
            UiScreenApi.SetScreenVisibility(UiScreenType.PausegameMenu, Booleans.False);
        }
        public void OnResumeClicked()
        {
            UiScreenApi.SetScreenVisibility(UiScreenType.PausegameMenu, Booleans.False);
        }
    }
}

using Assets.GameCode.Shared;
using Assets.GameCode.UiSystem.Data.Screens;
using Assets.GameCode.UiSystem.Logic;
using UnityEngine;

namespace Assets.GameCode.UiSystem.Components.Controllers
{
    [RequireComponent(typeof(UiScreenGameComponent))]
    public class GameScreenController: MonoBehaviour
    {
        public void OnMenuClicked()
        {
            UiScreenApi.SetScreenVisibility(UiScreenType.PausegameMenu, Booleans.True);
        }

        public void OnCheatboardClicked()
        {
            UiScreenApi.SetScreenVisibility(UiScreenType.Cheatboard, Booleans.True);
        }
    }
}

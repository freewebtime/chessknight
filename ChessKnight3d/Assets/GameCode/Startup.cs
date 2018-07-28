using Assets.GameCode.Shared;
using Assets.GameCode.UiSystem.Components.Controllers;
using Assets.GameCode.UiSystem.Data.Screens;
using Assets.GameCode.UiSystem.Logic;
using UnityEngine;

namespace Assets.GameCode
{
    public class Startup: MonoBehaviour
    {
        public UiRootController uiRootController;

        // game has been started
        protected void Start()
        {
            uiRootController.Initialize();

            UiScreenApi.SetScreenVisibility(UiScreenType.MainMenu, Booleans.True);
        }
    }
}

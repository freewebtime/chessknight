﻿using Assets.GameCode.Shared;
using Assets.GameCode.UiSystem.Data.Screens;
using Assets.GameCode.UiSystem.Logic;
using UnityEngine;

namespace Assets.GameCode.UiSystem.Components.Controllers
{
    [RequireComponent(typeof(UiScreenSettingsComponent))]
    public class SettingsController: MonoBehaviour
    {
        public void OnExitClicked()
        {
            UiScreenApi.SetAllScreensVisibility(Booleans.False);
            UiScreenApi.SetScreenVisibility(UiScreenType.MainMenu, Booleans.True);
        }
    }
}

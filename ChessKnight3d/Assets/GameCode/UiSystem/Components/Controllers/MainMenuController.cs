using UnityEngine;

namespace Assets.GameCode.UiSystem.Components.Controllers
{
    [RequireComponent(typeof(UiScreenMainMenuComponent))]
    public class MainMenuController: MonoBehaviour
    {
        public void OnPlayClicked()
        {

        }

        public void OnSettingsClicked()
        {

        }

        public void OnCreditsClicked()
        {

        }

        public void OnExitClicked()
        {
            Application.Quit();
        }
    }
}

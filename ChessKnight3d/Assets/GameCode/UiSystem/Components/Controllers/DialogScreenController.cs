using Assets.GameCode.Shared;
using Assets.GameCode.UiSystem.Data.Screens;
using Assets.GameCode.UiSystem.Logic;
using UnityEngine;

namespace Assets.GameCode.UiSystem.Components.Controllers
{
    [RequireComponent(typeof(UiScreenDialogComponent))]
    public class DialogScreenController: MonoBehaviour
    {
        public void OnOkClicked()
        {
            UiScreenApi.SetScreenVisibility(UiScreenType.Dialog, Booleans.False);
        }

        public void OnCancelClicked()
        {
            UiScreenApi.SetScreenVisibility(UiScreenType.Dialog, Booleans.False);
        }
    }
}

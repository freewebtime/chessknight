using Assets.GameCode.Shared.Extensions;
using Assets.GameCode.UiSystem.Data;
using TMPro;
using Unity.Entities;

namespace Assets.GameCode.UiSystem.Components
{
    public class ConfirmationDialogComponent: ComponentDataWrapper<ConfirmationDialog>
    {
        public GameObjectEntity gameObjectEntity;

        public TextMeshProUGUI Caption;

        public void OnOkClicked()
        {
            CloseDialog(DialogResults.Ok);
        }

        public void OnCancelClicked()
        {
            CloseDialog(DialogResults.Cancel);
        }

        private void CloseDialog(DialogResults dialogResult)
        {
            var myEntity = gameObjectEntity.Entity;
            var entityManager = gameObjectEntity.EntityManager;

            entityManager.AddOrSetComponentData(myEntity, new DialogResult { value = dialogResult });
            entityManager.AddOrSetComponentData(myEntity, new HideDialogRequest { });
        }
    }
}

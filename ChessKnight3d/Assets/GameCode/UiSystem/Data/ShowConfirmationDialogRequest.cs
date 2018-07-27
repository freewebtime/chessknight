using Unity.Entities;

namespace Assets.GameCode.UiSystem.Data
{
    public struct ShowConfirmationDialogRequest: ISharedComponentData
    {
        public string message;
    }
}

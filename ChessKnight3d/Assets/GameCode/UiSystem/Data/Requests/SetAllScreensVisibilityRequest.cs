using Unity.Entities;

namespace Assets.GameCode.UiSystem.Data.Requests
{
    public struct SetAllScreensVisibilityRequest : IComponentData
    {
        public byte isVisible;
    }
}

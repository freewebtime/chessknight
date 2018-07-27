using Assets.GameCode.UiSystem.Data.Screens;
using Unity.Entities;

namespace Assets.GameCode.UiSystem.Data.Requests
{
    public struct SetScreenVisibilityRequest : IComponentData
    {
        public UiScreenType screenType;
        public byte isVisible;
    }
}

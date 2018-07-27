using System;
using Unity.Entities;

namespace Assets.GameCode.UiSystem.Data.Screens
{
    [Serializable]
    public struct UiScreen: IComponentData
    {
        public UiScreenType type;
    }
}

using Assets.GameCode.UiSystem.Data.Screens;
using Unity.Entities;
using UnityEngine;

namespace Assets.GameCode.UiSystem.Components
{
    [RequireComponent(typeof(UiScreenComponent))]
    public class UiScreenDialogComponent: ComponentDataWrapper<UiScreenDialog> { }
}

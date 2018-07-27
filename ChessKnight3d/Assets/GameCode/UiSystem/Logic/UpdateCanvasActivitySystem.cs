using Assets.GameCode.Shared;
using Assets.GameCode.UiSystem.Data.Screens;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Assets.GameCode.UiSystem.Logic
{
    [UpdateInGroup(typeof(GameLoop.PreRendering))]
    public class UpdateCanvasActivitySystem : ComponentSystem
    {
        struct Visibles
        {
            public readonly int Length;
            [ReadOnly] public ComponentArray<Canvas> canvas;
            [ReadOnly] public ComponentDataArray<UiScreen> uiScreen;
            [ReadOnly] public ComponentDataArray<Visible> visible;
        }

        struct Invisibles
        {
            public readonly int Length;
            [ReadOnly] public ComponentArray<Canvas> canvas;
            [ReadOnly] public ComponentDataArray<UiScreen> uiScreen;
            public SubtractiveComponent<Visible> invisible;
        }

        [Inject] Visibles visibles;
        [Inject] Invisibles invisibles;

        protected override void OnUpdate()
        {
            for (int i = 0; i < visibles.Length; i++)
            {
                visibles.canvas[i].enabled = true;
            }

            for (int i = 0; i < invisibles.Length; i++)
            {
                invisibles.canvas[i].enabled = false;
            }
        }
    }
}

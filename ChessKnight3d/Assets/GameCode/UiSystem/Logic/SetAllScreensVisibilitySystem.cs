using Assets.GameCode.Shared;
using Assets.GameCode.UiSystem.Data.Requests;
using Assets.GameCode.UiSystem.Data.Screens;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Assets.GameCode.UiSystem.Logic
{
    [UpdateInGroup(typeof(GameLoop.PreRendering))]
    [UpdateBefore(typeof(SetScreenVisibilitySystem))]
    public class SetAllScreensVisibilitySystem : ComponentSystem
    {
        struct SetAllRequests
        {
            public readonly int Length;
            [ReadOnly] public ComponentDataArray<SetAllScreensVisibilityRequest> request;
            [ReadOnly] public EntityArray entity;
        }

        struct ScreenGroup
        {
            public readonly int Length;
            [ReadOnly] public EntityArray entity;
            [ReadOnly] public ComponentArray<Canvas> canvas;
            [ReadOnly] public ComponentDataArray<UiScreen> uiScreen;
        }

        [Inject] SetAllRequests setAllRequests;
        [Inject] ScreenGroup screenGroup;

        protected override void OnUpdate()
        {
            for (int i = 0; i < setAllRequests.Length; i++)
            {
                var request = setAllRequests.request[i];
                var entity = setAllRequests.entity[i];

                PostUpdateCommands.DestroyEntity(entity);

                SetAllScreensVisibility(request.isVisible == Booleans.True);
            }
        }

        public void SetAllScreensVisibility(bool isVisible)
        {
            for (int i = 0; i < screenGroup.Length; i++)
            {
                var entity = screenGroup.entity[i];
                if (isVisible)
                {
                    if (!EntityManager.HasComponent<Visible>(entity))
                    {
                        PostUpdateCommands.AddComponent(entity, new Visible());
                    }
                }
                else
                {
                    if (EntityManager.HasComponent<Visible>(entity))
                    {
                        PostUpdateCommands.RemoveComponent<Visible>(entity);
                    }
                }
            }
        }
    }
}

using System;
using Assets.GameCode.Shared.Extensions;
using Assets.GameCode.UiSystem.Data;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Assets.GameCode.UiSystem.Logic
{
    [UpdateInGroup(typeof(GameLoop.PreRendering))]
    public class ConfirmationDialogSystem : ComponentSystem
    {
        struct Dialogs
        {
            public readonly int Length;
            [ReadOnly] public EntityArray entity;
            [ReadOnly] public ComponentArray<Canvas> canvas;
            [ReadOnly] public ComponentDataArray<ConfirmationDialog> dialog;
        }

        struct ShowRequests
        {
            public readonly int Length;
            [ReadOnly] public EntityArray entity;
            [ReadOnly] public ComponentArray<Canvas> canvas;
            [ReadOnly] public ComponentDataArray<ConfirmationDialog> dialog;
            [ReadOnly] public ComponentDataArray<ShowDialogRequest> request;
        }
        struct HideRequests
        {
            public readonly int Length;
            [ReadOnly] public EntityArray entity;
            [ReadOnly] public ComponentArray<Canvas> canvas;
            [ReadOnly] public ComponentDataArray<ConfirmationDialog> dialog;
            [ReadOnly] public ComponentDataArray<HideDialogRequest> request;
        }

        struct ShowConfirmDlgRequests
        {
            public readonly int Length;
            [ReadOnly] public EntityArray entity;
            [ReadOnly] public SharedComponentDataArray<ShowConfirmationDialogRequest> request;
        }

        [Inject] Dialogs dialogs;
        [Inject] ShowRequests showRequests;
        [Inject] HideRequests hideRequests;
        [Inject] ShowConfirmDlgRequests showConfirmDlgRequests;

        protected override void OnUpdate()
        {
            CheckShowDialogRequests();
            CheckShowRequests();
            CheckHideRequests();
        }

        private void CheckShowDialogRequests()
        {
            for (int i = 0; i < showConfirmDlgRequests.Length; i++)
            {
                var request = showConfirmDlgRequests.request[i];
                ShowMessage(request.message, PostUpdateCommands);

                var entity = showConfirmDlgRequests.entity[i];
                PostUpdateCommands.DestroyEntity(entity);
            }
        }

        private void CheckShowRequests()
        {
            for (int i = 0; i < showRequests.Length; i++)
            {
                var entity = showRequests.entity[i];
                PostUpdateCommands.RemoveComponent<ShowDialogRequest>(entity);

                showRequests.canvas[i].enabled = true;
            }
        }

        private void CheckHideRequests()
        {
            for (int i = 0; i < hideRequests.Length; i++)
            {
                var entity = hideRequests.entity[i];
                PostUpdateCommands.RemoveComponent<HideDialogRequest>(entity);

                hideRequests.canvas[i].enabled = false;
            }
        }

        public void ShowMessage(string message, EntityCommandBuffer commandBuffer)
        {
            for (int i = 0; i < dialogs.Length; i++)
            {
                // 1. set message
                var entity = dialogs.entity[i];
                commandBuffer.AddOrSetSharedComponentData(EntityManager, entity, new DialogCaption { value = message });

                // 2. show dialog
                commandBuffer.AddOrSetComponentData(EntityManager, entity, new ShowDialogRequest { });
            }
        }
    }
}

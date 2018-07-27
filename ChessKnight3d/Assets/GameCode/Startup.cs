using Assets.GameCode.UiSystem.Components.Controllers;
using Assets.GameCode.UiSystem.Data.Requests;
using Assets.GameCode.UiSystem.Data.Screens;
using Unity.Entities;
using UnityEngine;

namespace Assets.GameCode
{
    public class Startup: MonoBehaviour
    {
        public UiRootController uiRootController;

        // game has been started
        protected void Start()
        {
            uiRootController.Initialize();

            ShowMainMenu();
        }

        private static void ShowMainMenu()
        {
            var entityManager = World.Active.GetOrCreateManager<EntityManager>();
            var requestEntity = entityManager.CreateEntity();
            entityManager.AddComponentData(requestEntity, new SetScreenVisibilityRequest
            {
                isVisible = 1,
                screenType = UiScreenType.MainMenu
            });
        }
    }
}

using Assets.GameCode.Map;
using Assets.GameCode.Map.Logic;
using Assets.GameCode.Shared;
using Assets.GameCode.UiSystem.Components.Controllers;
using Assets.GameCode.UiSystem.Data.Screens;
using Assets.GameCode.UiSystem.Logic;
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
            var entityManager = World.Active.GetOrCreateManager<EntityManager>();

            InitializeArchetypes(entityManager);
            InitializeEnvironment(entityManager);
            RunGame();
        }

        private void RunGame()
        {
            UiScreenApi.SetScreenVisibility(UiScreenType.MainMenu, Booleans.True);
        }

        private void InitializeEnvironment(EntityManager entityManager)
        {
            // initialize resources
            WorldMapApi.InitResources(entityManager);

            // initialize ui
            uiRootController.Initialize();

            // initialize user

            // initialize game map
            WorldMapApi.Initialize(entityManager);
        }

        private void InitializeArchetypes(EntityManager entityManager)
        {
            WorldMapArchetypes.Initialize(entityManager);
        }
    }
}

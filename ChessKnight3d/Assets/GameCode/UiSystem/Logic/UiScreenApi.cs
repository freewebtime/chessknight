using Assets.GameCode.Shared;
using Assets.GameCode.UiSystem.Data.Requests;
using Assets.GameCode.UiSystem.Data.Screens;
using Unity.Entities;

namespace Assets.GameCode.UiSystem.Logic
{
    public static class UiScreenApi
    {
        public static void SetScreenVisibility(UiScreenType screenType, Booleans isVisible)
        {
            var entityManager = World.Active.GetOrCreateManager<EntityManager>();
            var requestEntity = entityManager.CreateEntity();
            entityManager.AddComponentData(requestEntity, new SetScreenVisibilityRequest
            {
                isVisible = isVisible,
                screenType = screenType
            });
        }

        public static void SetAllScreensVisibility(Booleans isVisible)
        {
            var entityManager = World.Active.GetOrCreateManager<EntityManager>();
            var requestEntity = entityManager.CreateEntity();
            entityManager.AddComponentData(requestEntity, new SetAllScreensVisibilityRequest
            {
                isVisible = isVisible,
            });
        }
    }
}

using Unity.Entities;

namespace Assets.GameCode.Shared.Extensions
{
    public static class SystemExtensions
    {
        public static void AddOrSetComponentData<TComponent>(this EntityManager entityManager, Entity entity, TComponent data) where TComponent : struct, IComponentData
        {
            if (entityManager.HasComponent<TComponent>(entity))
            {
                entityManager.SetComponentData(entity, data);
            }
            else
            {
                entityManager.AddComponentData(entity, data);
            }
        }
        public static void AddOrSetSharedComponentData<TComponent>(this EntityManager entityManager, Entity entity, TComponent data) where TComponent : struct, ISharedComponentData
        {
            if (entityManager.HasComponent<TComponent>(entity))
            {
                entityManager.SetSharedComponentData(entity, data);
            }
            else
            {
                entityManager.AddSharedComponentData(entity, data);
            }
        }
        public static void AddOrSetComponentData<TComponent>(this EntityCommandBuffer commandBuffer, EntityManager entityManager, Entity entity, TComponent data) where TComponent : struct, IComponentData
        {
            if (entityManager.HasComponent<TComponent>(entity))
            {
                commandBuffer.SetComponent(entity, data);
            }
            else
            {
                commandBuffer.AddComponent(entity, data);
            }
        }
        public static void AddOrSetSharedComponentData<TComponent>(this EntityCommandBuffer commandBuffer, EntityManager entityManager, Entity entity, TComponent data) where TComponent : struct, ISharedComponentData
        {
            if (entityManager.HasComponent<TComponent>(entity))
            {
                commandBuffer.SetSharedComponent(entity, data);
            }
            else
            {
                commandBuffer.AddSharedComponent(entity, data);
            }
        }
    }
}

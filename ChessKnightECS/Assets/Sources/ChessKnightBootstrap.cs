using UnityEngine;
using ChessKnight.Controllers;
using Unity.Entities;
using Assets.Sources.Components;

namespace ChessKnight
{
    public class ChessKnightBootstrap
    {
        public ChessKnightSettings Settings;
        public UiController UiController;

        public static EntityArchetype UserArchetype;
        public static Entity UserEntity;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            Debug.Log("Initialize called");

            // This method creates archetypes for entities we will spawn frequently in this game.
            // Archetypes are optional but can speed up entity spawning substantially.

            var entityManager = World.Active.GetOrCreateManager<EntityManager>();

            UserArchetype = entityManager.CreateArchetype(ComponentType.Create<User>());
        }

        private static void CreateUser(EntityManager entityManager)
        {
            UserEntity = entityManager.CreateEntity(UserArchetype);
            entityManager.SetSharedComponentData(UserEntity, new User { Index = 0, Name = "Default User" });
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void InitializeWithScene()
        {
            Debug.Log("InitializeWithScene called");

            var entityManager = World.Active.GetOrCreateManager<EntityManager>();

            CreateUser(entityManager);
        }
    }
}



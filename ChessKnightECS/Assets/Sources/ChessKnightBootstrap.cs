using UnityEngine;
using Unity.Entities;

namespace ChessKnight
{
    public class ChessKnightBootstrap: MonoBehaviour
    {
        public static ChessKnightSettings Settings;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            // This method creates archetypes for entities we will spawn frequently in this game.
            // Archetypes are optional but can speed up entity spawning substantially.

            var entityManager = World.Active.GetOrCreateManager<EntityManager>();
        }

        // Begin a new game.
        public static void NewGame()
        {
            // Access the ECS entity manager
            var entityManager = World.Active.GetOrCreateManager<EntityManager>();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void InitializeWithScene()
        {
            var allSettings = Resources.FindObjectsOfTypeAll<ChessKnightSettings>();
            Settings = allSettings.Length > 0 ? allSettings[0] : null;
        }
    }
}



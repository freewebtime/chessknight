using ChessKnight.Application;
using ChessKnight.GameLevel;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace ChessKnight
{
    public class Startup
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void OnBeforeSceneLoad()
        {
            var entityManager = World.Active.GetOrCreateManager<EntityManager>();
            InitAppSystem.Init(entityManager);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void OnAfterSceneLoad()
        {
            var entityManager = World.Active.GetOrCreateManager<EntityManager>();

            //var loadMatchRequest = entityManager.CreateEntity(ComponentType.Create<LoadMatchRequest>());
            //entityManager.SetComponentData(loadMatchRequest, new LoadMatchRequest {
            //    deskSize = new int2(10, 10),
            //    roomSize = new int2(30, 30)
            //});

            var generateLevelRequest = new GenerateLevelRequest
            {
                roomSize = new int2(30, 20),
                deskSize = new int2(10, 10),
                deskOffset = new int2(10, 5),

                isFirstStepAnywhere = 1,
                isStartingOnDesk = 0,

                seed = (int)(Time.time * Random.value * 1000),

                blockChance = 0.1f,
                bombChance = 0.2f,
                starChance = 0.3f,
                lockChance = 0.4f,
            };

            var requestEntity = entityManager.CreateEntity(ComponentType.Create<GenerateLevelRequest>());
            entityManager.SetComponentData(requestEntity, generateLevelRequest);
        }
    }
}




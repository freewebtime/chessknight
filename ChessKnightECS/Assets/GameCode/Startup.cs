using ChessKnight.Application;
using ChessKnight.GameMatch;
using ChessKnight.Gameplay;
using toinfiniityandbeyond.Rendering2D;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Transforms2D;
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

            var loadMatchRequest = entityManager.CreateEntity(ComponentType.Create<LoadMatchRequest>());
            entityManager.SetComponentData(loadMatchRequest, new LoadMatchRequest {
                deskSize = new int2(10, 10),
                roomSize = new int2(30, 30)
            });
        }
    }
}




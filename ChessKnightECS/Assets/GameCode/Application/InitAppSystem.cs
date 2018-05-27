using ChessKnight.GameMatch;
using toinfiniityandbeyond.Rendering2D;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace ChessKnight.Application
{
    public class InitAppSystem: ComponentSystem
    {
        public static void Init(EntityManager entityManager)
        {
            // create archetypes
            var matchArchetype = entityManager.CreateArchetype(
                ComponentType.Create<Match>()
                );
            var playerArchetype = entityManager.CreateArchetype(
                ComponentType.Create<MatchPlayer>()
                );
            var roomArchetype = entityManager.CreateArchetype(
                ComponentType.Create<MatchRoom>()
                );
            var deskArchetype = entityManager.CreateArchetype(
                ComponentType.Create<MatchDesk>()
                );
            var deskItemArchetype = entityManager.CreateArchetype(
                ComponentType.Create<MatchDeskItem>(),
                ComponentType.Create<MatchDeskCoordinate>(),
                ComponentType.Create<MatchDeskPosition>(),
                ComponentType.Create<SpriteInstanceRenderer>(),
                ComponentType.Create<TransformMatrix>(),
                ComponentType.Create<Position>()
                );

            // create appConfig
            var appConfig = new AppConfig
            {
                MatchArchetype = matchArchetype,
                MatchRoomArchetype = roomArchetype,
                MatchDeskArchetype = deskArchetype,
                MatchDeskItemArchetype = deskItemArchetype,
                MatchPlayerArchetype = playerArchetype,
            };

            // get media config
            var mediaConfig = Resources.Load<MediaConfig>("MediaConfig");

            // create app entity
            var appEntity = entityManager.CreateEntity(
                ComponentType.Create<App>(),
                ComponentType.Create<AppConfig>(),
                ComponentType.Create<AppMediaConfig>()
                );

            entityManager.SetComponentData(appEntity, appConfig);
            entityManager.SetSharedComponentData(appEntity, new AppMediaConfig { MediaConfig = mediaConfig });
        }

        protected override void OnUpdate()
        {
        }
    }
}

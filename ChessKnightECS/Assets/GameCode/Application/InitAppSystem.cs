using ChessKnight.GameLevel;
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
            var levelArchetype = entityManager.CreateArchetype(
                ComponentType.Create<Level>()
                );
            var levelItemArchetype = entityManager.CreateArchetype(
                ComponentType.Create<LevelItem>(),
                ComponentType.Create<GridCoordinate>(),
                ComponentType.Create<Position>(),
                ComponentType.Create<TransformMatrix>(),
                ComponentType.Create<SpriteInstanceRenderer>()
                );

            // create appConfig
            var appConfig = new AppConfig
            {
                LevelArchetype = levelArchetype,
                LevelItemArchetype = levelItemArchetype
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

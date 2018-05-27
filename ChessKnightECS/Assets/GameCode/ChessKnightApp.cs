using ChessKnight.Gameplay;
using toinfiniityandbeyond.Rendering2D;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Transforms2D;
using UnityEngine;

namespace ChessKnight
{
    public class ChessKnightApp
    {
        public static ChessKnightConfig Config;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void OnBeforeSceneLoad()
        {
            var entityManager = World.Active.GetOrCreateManager<EntityManager>();

            Config = new ChessKnightConfig();
            Config.MediaConfig = Resources.Load<MediaConfig>("Settings");

            CreateArchetypes(entityManager);
        }

        private static void CreateArchetypes(EntityManager entityManager)
        {
            Config.DeskArchetype = entityManager.CreateArchetype(
                ComponentType.Create<GameDesk>(),
                ComponentType.Create<GameDeskSize>(),
                ComponentType.Create<Position2D>(),
                ComponentType.Create<TransformMatrix>()
            );

            Config.DeskItemArchetype = entityManager.CreateArchetype(
                ComponentType.Create<GameDeskItem>(),
                ComponentType.Create<LayerIndex>(),
                ComponentType.Create<Grid2dCoordinate>(),
                ComponentType.Create<Position>(),
                ComponentType.Create<Heading2D>(),
                ComponentType.Create<LocalRotation>(),
                ComponentType.Create<TransformMatrix>(),
                ComponentType.Create<SpriteInstanceRenderer>()
            );

            Config.DeskLayerArchetype = entityManager.CreateArchetype(
                ComponentType.Create<GameDeskLayer>(),
                ComponentType.Create<LocalPosition>(),
                ComponentType.Create<TransformMatrix>(),
                ComponentType.Create<TransformParent>()
            );
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void OnAfterSceneLoad()
        {
            var entityManager = World.Active.GetOrCreateManager<EntityManager>();
            FillBackground();
        }

        private static void FillBackground()
        {
            var entityManager = World.Active.GetOrCreateManager<EntityManager>();

            //create grid
            var gridWidth = 10;
            var gridHeight = 10;

            var gridEntity = entityManager.CreateEntity(Config.DeskArchetype);
            entityManager.SetComponentData(gridEntity, new GameDeskSize { Width = gridWidth, Height = gridHeight });

            // create layers
            var layersCount = 3;
            var layers = new Entity[layersCount];

            for (int i = 0; i < layersCount; i++)
            {
                var layerEntity = entityManager.CreateEntity(Config.DeskLayerArchetype);
                entityManager.SetComponentData(layerEntity, new GameDeskLayer { Index = i });
                entityManager.SetComponentData(layerEntity, new TransformParent { Value = gridEntity });
                entityManager.SetComponentData(layerEntity, new LocalPosition { Value = new float3(0, 0, i) });

                layers[i] = layerEntity;
            }

            var bgOffset = 20;

            // create cells
            for (int y = -bgOffset; y < gridHeight + bgOffset; y++)
            {
                for (int x = -bgOffset; x < gridWidth + bgOffset; x++)
                {
                    var layer = layers[0];
                    var sprite = Config.MediaConfig.BackgroundSprites[math.abs(x * gridWidth + y) % Config.MediaConfig.BackgroundSprites.Length];
                    var texture = sprite.texture;
                    var pivot = new float2(0.5f, 0.5f);
                    var pixelsPerUnit = (int)sprite.pixelsPerUnit;

                    var cellEntity = entityManager.CreateEntity(Config.DeskItemArchetype);
                    entityManager.SetComponentData(cellEntity, new Grid2dCoordinate { X = x, Y = y });
                    entityManager.SetComponentData(cellEntity, new Position { Value = new float3(x, y, 0) });
                    entityManager.SetComponentData(cellEntity, new Heading2D { Value = new float2(0, 1) });
                    entityManager.SetComponentData(cellEntity, new LocalRotation { Value = Quaternion.identity});
                    entityManager.SetSharedComponentData(cellEntity, new SpriteInstanceRenderer { sprite = texture, pivot = pivot, pixelsPerUnit = pixelsPerUnit });
                    entityManager.SetComponentData(cellEntity, new LayerIndex { Value = 0 });
                }
            }

        }
    }

    public class ChessKnightConfig
    {
        public EntityArchetype DeskArchetype;
        public EntityArchetype DeskItemArchetype;
        public EntityArchetype DeskLayerArchetype;
        public MediaConfig MediaConfig;
    }

}




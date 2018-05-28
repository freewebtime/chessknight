using ChessKnight.Application;
using System.Collections.Generic;
using toinfiniityandbeyond.Rendering2D;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace ChessKnight.GameLevel
{
    public struct LoadLevelRequest: IComponentData
    {
        public int levelBlueprintId;
    }

    public class LoadLevelSystem : ComponentSystem
    {
        ComponentGroup BlueprintsGroup;

        struct RequestGroup
        {
            [ReadOnly] public ComponentDataArray<LoadLevelRequest> Requests;
            [ReadOnly] public EntityArray Entities;
            public int Length;
        }

        struct AppGroup
        {
            [ReadOnly] public ComponentDataArray<AppConfig> AppConfigs;
            [ReadOnly] public SharedComponentDataArray<AppMediaConfig> MediaConfigs;
            public int Length;
        }

        [Inject] RequestGroup requestGroup;
        [Inject] AppGroup appGroup;

        protected override void OnCreateManager(int capacity)
        {
            BlueprintsGroup = GetComponentGroup(
                ComponentType.Create<LevelBlueprint>(),
                ComponentType.Create<LevelBlueprintId>()
                );

            base.OnCreateManager(capacity);
        }

        protected override void OnUpdate()
        {
            // get app data
            if (appGroup.Length <= 0)
            {
                return;
            }

            var appConfig = appGroup.AppConfigs[0];
            var mediaConfig = appGroup.MediaConfigs[0];

            for (int i = 0; i < requestGroup.Length; i++)
            {
                var request = requestGroup.Requests[i];
                var levelBlueprintId = request.levelBlueprintId;

                // get blueprint
                BlueprintsGroup.SetFilter(new LevelBlueprintId
                {
                    levelId = levelBlueprintId
                });
                var blueprints = BlueprintsGroup.GetSharedComponentDataArray<LevelBlueprint>();
                if (blueprints.Length <= 0)
                {
                    return;
                }

                var blueprint = blueprints[0];

                // destroy request entity
                var entity = requestGroup.Entities[i];
                PostUpdateCommands.DestroyEntity(entity);

                // 1. create level
                var levelEntity = EntityManager.CreateEntity(appConfig.LevelArchetype);

                // 2. create level items
                var levelItemsCount = blueprint.levelItems.Length;
                for (int li = 0; li < levelItemsCount; li++)
                {
                    var liBlueprint = blueprint.levelItems[li];
                    var liPosition = new float3(liBlueprint.coordinate.x, liBlueprint.coordinate.y, -(int)liBlueprint.itemType);

                    var itemSprites = mediaConfig.MediaConfig.ItemTypeSprites[(int)liBlueprint.itemType];
                    Sprite sprite = null;
                    if (itemSprites.Sprites.Length > liBlueprint.version)
                    {
                        sprite = itemSprites.Sprites[liBlueprint.version];
                        liPosition += new float3(itemSprites.Offset.x, itemSprites.Offset.y, 0);
                    }

                    var levelItemEntity = EntityManager.CreateEntity(appConfig.LevelItemArchetype);
                    EntityManager.SetComponentData(levelItemEntity, new LevelItem {
                        ItemType = liBlueprint.itemType,
                        Version = liBlueprint.version,
                    });
                    EntityManager.SetSharedComponentData(levelItemEntity, new GridCoordinate
                    {
                        coordinate = liBlueprint.coordinate
                    });
                    EntityManager.SetComponentData(levelItemEntity, new Position
                    {
                        Value = liPosition
                    });

                    if (sprite != null)
                    {
                        EntityManager.SetSharedComponentData(levelItemEntity, new SpriteInstanceRenderer
                        {
                            sprite = sprite.texture,
                            pivot = new float2(0.5f, 0.5f),
                            pixelsPerUnit = sprite.pixelsPerUnit,
                        });
                    }
                }
            }
        }
    }
}

using Assets.GameCode.Configs.Map;
using Assets.GameCode.Map.Data.Events;
using Assets.GameCode.Map.Data.Resources;
using Unity.Collections;
using Unity.Entities;

namespace Assets.GameCode.Map.Logic
{
    public class InitMapResourcesSystem : ComponentSystem
    {
        struct Requests
        {
            public readonly int Length;
            [ReadOnly] public EntityArray entity;
            [ReadOnly] public SharedComponentDataArray<InitMapResourcesLibRequest> request;
        }

        [Inject] Requests requests;

        protected override void OnUpdate()
        {
            for (int i = 0; i < requests.Length; i++)
            {
                PostUpdateCommands.DestroyEntity(requests.entity[i]);

                var request = requests.request[i];
                var config = request.config;
                var mapPacksConfigs = config.mapResourcePacks;

                var mapPacks = new NativeArray<Entity>(mapPacksConfigs.Length, Allocator.Persistent);

                // create map packs
                for (int j = 0; j < mapPacksConfigs.Length; j++)
                {
                    var mapPackConfig = config.mapResourcePacks[j];
                    var mapItemsConfigs = mapPackConfig.mapItems;
                    var groundTypeConfigs = mapPackConfig.groundTypeConfigs;

                    var mapItems = new NativeArray<Entity>(mapItemsConfigs.Length, Allocator.Persistent);
                    var groundTypes = new NativeArray<GroundTypeConfig>(groundTypeConfigs.Length, Allocator.Persistent);

                    // create ground types
                    for (int k = 0; k < groundTypes.Length; k++)
                    {
                        groundTypes[i] = groundTypeConfigs[k];
                    }

                    // create map items
                    for (int k = 0; k < mapItemsConfigs.Length; k++)
                    {
                        var mapItemConfig = mapItemsConfigs[k];
                        var mapItemEntity = EntityManager.CreateEntity(WorldMapArchetypes.mapItemResourcePack);
                        EntityManager.SetSharedComponentData(mapItemEntity, new MapItemResourcePack {
                            id = mapItemConfig.id,
                            material = mapItemConfig.material,
                            mesh = mapItemConfig.mesh,
                            position = mapItemConfig.position,
                            rotation = mapItemConfig.rotation,
                            scale = mapItemConfig.scale
                        });

                        mapItems[k] = mapItemEntity;
                    }

                    var mapPackEntity = EntityManager.CreateEntity(WorldMapArchetypes.mapResourcePack);
                    EntityManager.SetSharedComponentData(mapPackEntity, new MapResourcePack {
                        groundMaterial = mapPackConfig.groundMaterial,
                        waterMapterial = mapPackConfig.waterMaterial,
                        mapItems = mapItems,
                        groundTypeConfigs = groundTypes
                    });
                    mapPacks[j] = mapPackEntity;
                }

                // create lib
                var libEntity = EntityManager.CreateEntity(WorldMapArchetypes.mapResourceLib);
                EntityManager.SetSharedComponentData(libEntity, new MapResourcesLib {
                    mapPacks = mapPacks
                });
            }
        }
    }
}

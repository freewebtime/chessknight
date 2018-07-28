using Assets.GameCode.Configs.Map;
using Assets.GameCode.Map.Data.Events;
using Assets.GameCode.Shared;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.GameCode.Map.Logic
{
    public static class WorldMapApi
    {
        public static void InitResources(EntityManager entityManager)
        {
            // load configs
            var resourceLibConfig = Resources.Load<MapResourcesLibConfig>("mapResources");
            if (resourceLibConfig)
            {
                return;
            }

            // init configs
            for (int i = 0; i < resourceLibConfig.mapResourcePacks.Length; i++)
            {
                var mapPack = resourceLibConfig.mapResourcePacks[i];
                mapPack.id = i;
                mapPack.groundTypeConfigs = new GroundTypeConfig[mapPack.groundTypeSprites.Length];

                for (int j = 0; j < mapPack.groundTypeSprites.Length; j++)
                {
                    var sprite = mapPack.groundTypeSprites[j];
                    var spriteUv = new SpriteUv
                    {
                        uv00 = sprite.uv[0],
                        uv01 = sprite.uv[1],
                        uv11 = sprite.uv[2],
                        uv10 = sprite.uv[3]
                    };
                }

                for (int j = 0; j < mapPack.mapItems.Length; j++)
                {
                    var mapItem = mapPack.mapItems[j];
                    mapItem.id = j;
                }
            }

            // create map item packs
            var requestEntity = entityManager.CreateEntity(WorldMapArchetypes.initMapResourcesLibRequest);
            entityManager.SetSharedComponentData(requestEntity, new InitMapResourcesLibRequest {
                config = resourceLibConfig
            });
        }

        public static void Initialize(EntityManager entityManager)
        {
            // create world map
            var requestEntity = entityManager.CreateEntity(WorldMapArchetypes.createMapRequest);
            entityManager.SetComponentData(requestEntity, new CreateMapRequest {
                chunkSize = new int2(10, 10),
                sizeInChunks = new int2(40, 20),
                mapResourcesIndex = 0,
            });
        }
    }
}

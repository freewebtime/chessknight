using Assets.GameCode.Configs;
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
            var programConfig = Resources.Load<ProgramConfig>("config");
            if (!programConfig)
            {
                return;
            }

            // init configs
            var mapConfigs = programConfig.mapConfigs;
            for (int i = 0; i < mapConfigs.Length; i++)
            {
                // id
                var mapPack = mapConfigs[i];
                mapPack.id = i;

                // ground types
                var groundTypes = mapPack.groundTypes;
                for (int j = 0; j < groundTypes.Length; j++)
                {
                    var groundType = groundTypes[j];
                    var sprite = groundType.sprite;

                    groundType.id = j;
                    groundType.uv = new SpriteUv
                    {
                        uv00 = sprite.uv[0],
                        uv01 = sprite.uv[1],
                        uv11 = sprite.uv[2],
                        uv10 = sprite.uv[3]
                    };
                }

                // map items
                var mapItems = mapPack.mapItems;
                for (int j = 0; j < mapItems.Length; j++)
                {
                    var mapItem = mapItems[j];
                    mapItem.id = j;
                }
            }

            ProgramConfig.config = programConfig;
        }

        public static void Initialize(EntityManager entityManager)
        {
            // create world map
            var requestEntity = entityManager.CreateEntity(WorldMapArchetypes.createMapRequest);
            entityManager.SetComponentData(requestEntity, new CreateMapRequest {
                chunkSize = new int2(2, 2),
                sizeInChunks = new int2(1, 1),
                mapResourcesIndex = 0
            });
            entityManager.AddComponentData(requestEntity, new GenerateMapRequest {
                noiseScale = new float2(0.05f, 0.05f),
                randomSeed = 100500
            });
        }
    }
}

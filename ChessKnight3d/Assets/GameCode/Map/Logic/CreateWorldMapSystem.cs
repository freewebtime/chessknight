using Assets.GameCode.Map.Data;
using Assets.GameCode.Map.Data.Events;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.GameCode.Map.Logic
{
    [UpdateInGroup(typeof(GameLoop.ProcessingInput))]
    public class CreateWorldMapSystem : ComponentSystem
    {
        struct RequestGroup
        {
            public readonly int Length;
            [ReadOnly] public EntityArray entity;
            [ReadOnly] public ComponentDataArray<CreateMapRequest> request;
        }

        [Inject] RequestGroup requestGroup;

        [Inject] ComponentDataFromEntity<GenerateMapRequest> generateMapRequests;

        protected override void OnUpdate()
        {
            var mapArchetype = WorldMapArchetypes.worldMap;
            var mapChunkArchetype = WorldMapArchetypes.mapChunk;
            
            for (int i = 0; i < requestGroup.Length; i++)
            {
                var requestEntity = requestGroup.entity[i];
                PostUpdateCommands.DestroyEntity(requestEntity);

                var request = requestGroup.request[i];
                var chunkSize = request.chunkSize;
                var sizeInChunks = request.sizeInChunks;
                var mapSize = new int2(chunkSize.x * sizeInChunks.x, chunkSize.y * sizeInChunks.y);
                var chunksCount = sizeInChunks.x * sizeInChunks.y;
                var chunkCellsCount = chunkSize.x * chunkSize.y;

                var isGenerateMap = generateMapRequests.Exists(requestEntity);
                var generateMapRequest = default(GenerateMapRequest);
                if (isGenerateMap)
                {
                    generateMapRequest = generateMapRequests[requestEntity];
                }

                // create map
                var mapEntity = EntityManager.CreateEntity(mapArchetype);

                var chunksmap = new NativeArray<Entity>(chunksCount, Allocator.Persistent);

                // create all the chunks
                for (int y = 0; y < sizeInChunks.y; y++)
                {
                    for (int x = 0; x < sizeInChunks.x; x++)
                    {
                        var chunkEntity = EntityManager.CreateEntity(mapChunkArchetype);
                        EntityManager.SetSharedComponentData(chunkEntity, new Heightmap
                        {
                            height01 = new NativeArray<byte>(chunkCellsCount, Allocator.Persistent),
                            height10 = new NativeArray<byte>(chunkCellsCount, Allocator.Persistent),
                            height00 = new NativeArray<byte>(chunkCellsCount, Allocator.Persistent),
                            height11 = new NativeArray<byte>(chunkCellsCount, Allocator.Persistent),
                            center = new NativeArray<float>(chunkCellsCount, Allocator.Persistent)
                        });
                        EntityManager.SetSharedComponentData(chunkEntity, new Watermap
                        {
                            height01 = new NativeArray<byte>(chunkCellsCount, Allocator.Persistent),
                            height10 = new NativeArray<byte>(chunkCellsCount, Allocator.Persistent),
                            height00 = new NativeArray<byte>(chunkCellsCount, Allocator.Persistent),
                            height11 = new NativeArray<byte>(chunkCellsCount, Allocator.Persistent),
                            center = new NativeArray<float>(chunkCellsCount, Allocator.Persistent)
                        });
                        EntityManager.SetSharedComponentData(chunkEntity, new Groundmap
                        {
                            east = new NativeArray<byte>(chunkCellsCount, Allocator.Persistent),
                            west = new NativeArray<byte>(chunkCellsCount, Allocator.Persistent),
                            north = new NativeArray<byte>(chunkCellsCount, Allocator.Persistent),
                            south = new NativeArray<byte>(chunkCellsCount, Allocator.Persistent)
                        });
                        EntityManager.SetSharedComponentData(chunkEntity, new Bordermap
                        {
                            east = new NativeArray<byte>(chunkCellsCount, Allocator.Persistent),
                            west = new NativeArray<byte>(chunkCellsCount, Allocator.Persistent),
                            north = new NativeArray<byte>(chunkCellsCount, Allocator.Persistent),
                            south = new NativeArray<byte>(chunkCellsCount, Allocator.Persistent)
                        });
                        EntityManager.SetSharedComponentData(chunkEntity, new Itemsmap
                        {
                            value = new NativeArray<int>(chunkCellsCount, Allocator.Persistent),
                        });
                        EntityManager.SetSharedComponentData(chunkEntity, new ItemsTransformmap
                        {
                            matrix = new NativeArray<Matrix4x4>(chunkCellsCount, Allocator.Persistent),
                            position = new NativeArray<float3>(chunkCellsCount, Allocator.Persistent),
                            rotation = new NativeArray<float3>(chunkCellsCount, Allocator.Persistent),
                            scale = new NativeArray<float3>(chunkCellsCount, Allocator.Persistent)
                        });

                        EntityManager.SetComponentData(chunkEntity, new WorldMapRef
                        {
                            target = mapEntity
                        });

                        var chunkIndex = x + y * sizeInChunks.x;
                        var chunkCoordinate = new int2(x, y);
                        var chunkPosition = new int2(x * chunkSize.x, y * chunkSize.y);

                        EntityManager.SetComponentData(chunkEntity, new MapChunk
                        {
                            chunkIndex = chunkIndex,
                            coordinate = chunkCoordinate,
                            position = chunkPosition,
                            size = chunkSize
                        });

                        if (isGenerateMap)
                        {
                            EntityManager.AddComponentData(chunkEntity, generateMapRequest);
                        }

                        chunksmap[chunkIndex] = chunkEntity;
                    }
                }

                EntityManager.SetSharedComponentData(mapEntity, new Chunksmap
                {
                    value = chunksmap
                });
                EntityManager.SetComponentData(mapEntity, new MapResourcesIndex
                {
                    value = request.mapResourcesIndex
                });
            }
        }
    }
}

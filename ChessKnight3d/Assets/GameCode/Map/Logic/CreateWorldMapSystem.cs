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

        protected override void OnUpdate()
        {
            var mapArchetype = WorldMapArchetypes.worldMap;
            var mapChunkArchetype = WorldMapArchetypes.mapChunk;
            
            for (int i = 0; i < requestGroup.Length; i++)
            {
                PostUpdateCommands.DestroyEntity(requestGroup.entity[i]);

                var request = requestGroup.request[i];
                var chunkSize = request.chunkSize;
                var sizeInChunks = request.sizeInChunks;
                var mapSize = new int2(chunkSize.x * sizeInChunks.x, chunkSize.y * sizeInChunks.y);
                var chunksCount = sizeInChunks.x * sizeInChunks.y;
                var chunkCellsCount = chunkSize.x * chunkSize.y;

                // create map
                var mapEntity = EntityManager.CreateEntity(mapArchetype);

                var chunksmap = new NativeArray<Entity>(chunksCount, Allocator.Persistent);

                // create all the chunks
                for (int y = 0; y < sizeInChunks.y; y++)
                {
                    for (int x = 0; x < sizeInChunks.x; x++)
                    {
                        var eastHeightmap = new NativeArray<byte>(chunkCellsCount, Allocator.Persistent);
                        var westHeightmap = new NativeArray<byte>(chunkCellsCount, Allocator.Persistent);
                        var northHeightmap = new NativeArray<byte>(chunkCellsCount, Allocator.Persistent);
                        var southHeightmap = new NativeArray<byte>(chunkCellsCount, Allocator.Persistent);

                        var eastWatermap = new NativeArray<byte>(chunkCellsCount, Allocator.Persistent);
                        var westWatermap = new NativeArray<byte>(chunkCellsCount, Allocator.Persistent);
                        var northWatermap = new NativeArray<byte>(chunkCellsCount, Allocator.Persistent);
                        var southWatermap = new NativeArray<byte>(chunkCellsCount, Allocator.Persistent);

                        var eastGroundmap = new NativeArray<byte>(chunkCellsCount, Allocator.Persistent);
                        var westGroundmap = new NativeArray<byte>(chunkCellsCount, Allocator.Persistent);
                        var northGroundmap = new NativeArray<byte>(chunkCellsCount, Allocator.Persistent);
                        var southGroundmap = new NativeArray<byte>(chunkCellsCount, Allocator.Persistent);

                        var eastBordermap = new NativeArray<byte>(chunkCellsCount, Allocator.Persistent);
                        var westBordermap = new NativeArray<byte>(chunkCellsCount, Allocator.Persistent);
                        var northBordermap = new NativeArray<byte>(chunkCellsCount, Allocator.Persistent);
                        var southBordermap = new NativeArray<byte>(chunkCellsCount, Allocator.Persistent);

                        var itemsId = new NativeArray<int>(chunkCellsCount, Allocator.Persistent);
                        var itemsPosition = new NativeArray<float3>(chunkCellsCount, Allocator.Persistent);
                        var itemsRotation = new NativeArray<float3>(chunkCellsCount, Allocator.Persistent);
                        var itemsScale = new NativeArray<float3>(chunkCellsCount, Allocator.Persistent);

                        var itemsTransforms = new NativeArray<Matrix4x4>(chunkCellsCount, Allocator.Persistent);

                        var chunkEntity = EntityManager.CreateEntity(mapChunkArchetype);
                        EntityManager.SetSharedComponentData(chunkEntity, new Heightmap
                        {
                            east = eastHeightmap,
                            west = westHeightmap,
                            north = northHeightmap,
                            south = southHeightmap
                        });
                        EntityManager.SetSharedComponentData(chunkEntity, new Watermap
                        {
                            east = eastWatermap,
                            west = westWatermap,
                            north = northWatermap,
                            south = southWatermap
                        });
                        EntityManager.SetSharedComponentData(chunkEntity, new Groundmap
                        {
                            east = eastGroundmap,
                            west = westGroundmap,
                            north = northGroundmap,
                            south = southGroundmap
                        });
                        EntityManager.SetSharedComponentData(chunkEntity, new Bordermap
                        {
                            east = eastBordermap,
                            west = westBordermap,
                            north = northBordermap,
                            south = southBordermap
                        });
                        EntityManager.SetSharedComponentData(chunkEntity, new Itemsmap
                        {
                            id = itemsId,
                            position = itemsPosition,
                            rotation = itemsRotation,
                            scale = itemsScale
                        });
                        EntityManager.SetSharedComponentData(chunkEntity, new ItemsTransforms
                        {
                            value = itemsTransforms
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

                        chunksmap[chunkIndex] = chunkEntity;
                    }
                }

                EntityManager.SetSharedComponentData(mapEntity, new Chunksmap {
                    value = chunksmap
                });
                EntityManager.SetComponentData(mapEntity, new MapResourcesIndex {
                    value = request.mapResourcesIndex
                });
            }
        }
    }
}

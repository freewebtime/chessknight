using Assets.GameCode.Configs;
using Assets.GameCode.Map.Data;
using Assets.GameCode.Map.Data.Events;
using Assets.GameCode.Shared;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.GameCode.Map.Logic
{
    [UpdateInGroup(typeof(GameLoop.ProcessingInput))]
    [UpdateAfter(typeof(CreateWorldMapSystem))]
    public class GenerateMapSystem : ComponentSystem
    {
        struct ChunkGroup
        {
            public readonly int Length;
            [ReadOnly] public EntityArray entity;
            [ReadOnly] public ComponentDataArray<GenerateMapRequest> request;
            [ReadOnly] public ComponentDataArray<MapChunk> chunk;
            [ReadOnly] public SharedComponentDataArray<Heightmap> heightmap;
            [ReadOnly] public SharedComponentDataArray<Groundmap> groundmap;
            [ReadOnly] public SharedComponentDataArray<Watermap> watermap;
            [ReadOnly] public SharedComponentDataArray<Itemsmap> itemsmap;
            [ReadOnly] public SharedComponentDataArray<Bordermap> bordermap;
            [ReadOnly] public SharedComponentDataArray<ItemsTransformmap> itemsTransformmap;
        }

        [Inject] ChunkGroup chunkGroup;

        protected override void OnUpdate()
        {
            var mapConfig = ProgramConfig.config.mapConfigs[0];
            var groundConfigs = mapConfig.groundTypes;
            var groundConfigsCount = groundConfigs.Length;
            var itemsConfigs = mapConfig.mapItems;
            var itemsConfigsCount = itemsConfigs.Length;

            for (int i = 0; i < chunkGroup.Length; i++)
            {
                var chunkEntity = chunkGroup.entity[i];
                PostUpdateCommands.RemoveComponent<GenerateMapRequest>(chunkEntity);

                var request = chunkGroup.request[i];
                var chunk = chunkGroup.chunk[i];

                var chunkSize = chunk.size;
                var cellsCount = chunkSize.x * chunkSize.y;
                var chunkPosition = chunk.position;
                var noiseScale = request.noiseScale;
                var gridSize = chunkSize;
                var mapHeight = 255;
                var seaLevel = request.seaLevel;

                // dispose old data
                var oldHeightmap = chunkGroup.heightmap[i];
                var oldGroundmap = chunkGroup.groundmap[i];
                var oldBordermap = chunkGroup.bordermap[i];
                var oldWatermap = chunkGroup.watermap[i];
                var oldItemsmap = chunkGroup.itemsmap[i];
                var oldTransformsmap = chunkGroup.itemsTransformmap[i];

                if (oldHeightmap.height10.IsCreated)
                    oldHeightmap.height10.Dispose();
                if (oldHeightmap.height00.IsCreated)
                    oldHeightmap.height00.Dispose();
                if (oldHeightmap.height01.IsCreated)
                    oldHeightmap.height01.Dispose();
                if (oldHeightmap.height11.IsCreated)
                    oldHeightmap.height11.Dispose();
                if (oldHeightmap.center.IsCreated)
                    oldHeightmap.center.Dispose();

                if (oldWatermap.height00.IsCreated)
                    oldWatermap.height00.Dispose();
                if (oldWatermap.height01.IsCreated)
                    oldWatermap.height01.Dispose();
                if (oldWatermap.height11.IsCreated)
                    oldWatermap.height11.Dispose();
                if (oldWatermap.height10.IsCreated)
                    oldWatermap.height10.Dispose();
                if (oldWatermap.center.IsCreated)
                    oldWatermap.center.Dispose();

                if (oldGroundmap.west.IsCreated)
                    oldGroundmap.west.Dispose();
                if (oldGroundmap.north.IsCreated)
                    oldGroundmap.north.Dispose();
                if (oldGroundmap.east.IsCreated)
                    oldGroundmap.east.Dispose();
                if (oldGroundmap.south.IsCreated)
                    oldGroundmap.south.Dispose();

                if (oldBordermap.west.IsCreated)
                    oldBordermap.west.Dispose();
                if (oldBordermap.north.IsCreated)
                    oldBordermap.north.Dispose();
                if (oldBordermap.east.IsCreated)
                    oldBordermap.east.Dispose();
                if (oldBordermap.south.IsCreated)
                    oldBordermap.south.Dispose();

                if (oldItemsmap.value.IsCreated)
                    oldItemsmap.value.Dispose();

                if (oldTransformsmap.matrix.IsCreated)
                    oldTransformsmap.matrix.Dispose();
                if (oldTransformsmap.position.IsCreated)
                    oldTransformsmap.position.Dispose();
                if (oldTransformsmap.rotation.IsCreated)
                    oldTransformsmap.rotation.Dispose();
                if (oldTransformsmap.scale.IsCreated)
                    oldTransformsmap.scale.Dispose();

                // create new data
                var heightmap = new Heightmap
                {
                    height01 = new NativeArray<byte>(cellsCount, Allocator.Persistent),
                    height10 = new NativeArray<byte>(cellsCount, Allocator.Persistent),
                    height00 = new NativeArray<byte>(cellsCount, Allocator.Persistent),
                    height11 = new NativeArray<byte>(cellsCount, Allocator.Persistent),
                    center = new NativeArray<float>(cellsCount, Allocator.Persistent),
                };
                var watermap = new Watermap
                {
                    height11 = new NativeArray<byte>(cellsCount, Allocator.Persistent),
                    height00 = new NativeArray<byte>(cellsCount, Allocator.Persistent),
                    height01 = new NativeArray<byte>(cellsCount, Allocator.Persistent),
                    height10 = new NativeArray<byte>(cellsCount, Allocator.Persistent),
                    center = new NativeArray<float>(cellsCount, Allocator.Persistent),
                };
                var bordermap = new Bordermap
                {
                    east = new NativeArray<byte>(cellsCount, Allocator.Persistent),
                    west = new NativeArray<byte>(cellsCount, Allocator.Persistent),
                    north = new NativeArray<byte>(cellsCount, Allocator.Persistent),
                    south = new NativeArray<byte>(cellsCount, Allocator.Persistent),
                };
                var groundmap = new Groundmap
                {
                    east = new NativeArray<byte>(cellsCount, Allocator.Persistent),
                    west = new NativeArray<byte>(cellsCount, Allocator.Persistent),
                    north = new NativeArray<byte>(cellsCount, Allocator.Persistent),
                    south = new NativeArray<byte>(cellsCount, Allocator.Persistent),
                };
                var itemsmap = new Itemsmap
                {
                    value = new NativeArray<int>(cellsCount, Allocator.Persistent),
                };
                var itemsTransformmap = new ItemsTransformmap
                {
                    position = new NativeArray<float3>(cellsCount, Allocator.Persistent),
                    rotation = new NativeArray<float3>(cellsCount, Allocator.Persistent),
                    scale = new NativeArray<float3>(cellsCount, Allocator.Persistent),
                    matrix = new NativeArray<Matrix4x4>(cellsCount, Allocator.Persistent),
                };

                // fill data with values
                for (int y = 0; y < chunkSize.y; y++)
                {
                    for (int x = 0; x < chunkSize.x; x++)
                    {
                        var coord = new int2(x, y) + chunkPosition;
                        var cellIndex = y * gridSize.x + x;

                        // set heights
                        float fHeight00 = seaLevel + 2;
                        float fHeight01 = seaLevel + 2;
                        float fHeight11 = seaLevel + 2;
                        float fHeight10 = seaLevel + 2;

                        if (request.isFlatMap != Booleans.True)
                        {
                            var noiseSeed00 = new float2(coord.x * noiseScale.x, coord.y * noiseScale.y) + new float2(0f, 0f);
                            var noiseResult00 = noise.cnoise(noiseSeed00);
                            var noiseSeed01 = new float2(coord.x * noiseScale.x, coord.y * noiseScale.y) + new float2(0f, noiseScale.y / 2f);
                            var noiseResult01 = noise.cnoise(noiseSeed01);
                            var noiseSeed11 = new float2(coord.x * noiseScale.x, coord.y * noiseScale.y) + new float2(noiseScale.x / 2f, noiseScale.y / 2f);
                            var noiseResult11 = noise.cnoise(noiseSeed11);
                            var noiseSeed10 = new float2(coord.x * noiseScale.x, coord.y * noiseScale.y) + new float2(noiseScale.x / 2f, 0f);
                            var noiseResult10 = noise.cnoise(noiseSeed10);

                            fHeight00 = noiseResult00 * mapHeight / 2f + mapHeight / 2f;
                            fHeight01 = noiseResult01 * mapHeight / 2f + mapHeight / 2f;
                            fHeight11 = noiseResult11 * mapHeight / 2f + mapHeight / 2f;
                            fHeight10 = noiseResult10 * mapHeight / 2f + mapHeight / 2f;
                        }

                        float fHeightCenter = (fHeight00 + fHeight11 + fHeight01 + fHeight10) / 4f;
                        float minHeight = math.min(fHeight00, math.min(fHeight01, math.min(fHeight10, fHeight11)));

                        heightmap.height00[cellIndex] = (byte)fHeight00;
                        heightmap.height01[cellIndex] = (byte)fHeight01;
                        heightmap.height11[cellIndex] = (byte)fHeight11;
                        heightmap.height10[cellIndex] = (byte)fHeight10;
                        heightmap.center[cellIndex] = fHeightCenter;

                        // set water
                        float wHeight00 = math.max(0, seaLevel - fHeight00);
                        float wHeight01 = math.max(0, seaLevel - fHeight01);
                        float wHeight11 = math.max(0, seaLevel - fHeight11);
                        float wHeight10 = math.max(0, seaLevel - fHeight10);

                        watermap.height01[cellIndex] = (byte)fHeight00;
                        watermap.height11[cellIndex] = (byte)fHeight01;
                        watermap.height10[cellIndex] = (byte)fHeight11;
                        watermap.height00[cellIndex] = (byte)fHeight10;

                        // set ground id
                        var groundIdNorth = Random.Range(0, groundConfigsCount);
                        var groundIdEast = Random.Range(0, groundConfigsCount);
                        var groundIdSouth = Random.Range(0, groundConfigsCount);
                        var groundIdWest = Random.Range(0, groundConfigsCount);

                        groundmap.north[cellIndex] = (byte)groundIdNorth;
                        groundmap.east[cellIndex] = (byte)groundIdEast;
                        groundmap.south[cellIndex] = (byte)groundIdSouth;
                        groundmap.west[cellIndex] = (byte)groundIdWest;

                        // set borders
                        byte borderIdNorth = 0;
                        byte borderIdEast = 0;
                        byte borderIdSouth = 0;
                        byte borderIdWest = 0;

                        bordermap.north[cellIndex] = borderIdNorth;
                        bordermap.south[cellIndex] = borderIdSouth;
                        bordermap.east[cellIndex] = borderIdEast;
                        bordermap.west[cellIndex] = borderIdWest;

                        // set item
                        var itemId = 0;
                        if (minHeight > seaLevel)
                        {
                            if (Random.Range(0, 100) < 30)
                            {
                                itemId = Random.Range(0, itemsConfigsCount) + 1; // item can't be 0, cause 0 means no item
                                itemsmap.value[cellIndex] = itemId;

                                // set item transform
                                itemsTransformmap.position[cellIndex] = new float3();
                                itemsTransformmap.rotation[cellIndex] = new float3();
                                itemsTransformmap.scale[cellIndex] = new float3();
                                itemsTransformmap.matrix[cellIndex] = Matrix4x4.identity;
                            }
                        }
                    }
                }

                // save data
                PostUpdateCommands.SetSharedComponent(chunkEntity, heightmap);
                PostUpdateCommands.SetSharedComponent(chunkEntity, watermap);
                PostUpdateCommands.SetSharedComponent(chunkEntity, groundmap);
                PostUpdateCommands.SetSharedComponent(chunkEntity, bordermap);
                PostUpdateCommands.SetSharedComponent(chunkEntity, itemsmap);
                PostUpdateCommands.SetSharedComponent(chunkEntity, itemsTransformmap);

                // mark as dirty
                if (EntityManager.HasComponent<GroundmeshDirty>(chunkEntity))
                    PostUpdateCommands.AddComponent(chunkEntity, new GroundmeshDirty { });
                if (EntityManager.HasComponent<WatermeshDirty>(chunkEntity))
                    PostUpdateCommands.AddComponent(chunkEntity, new WatermeshDirty { });
                if (EntityManager.HasComponent<BordermeshDirty>(chunkEntity))
                    PostUpdateCommands.AddComponent(chunkEntity, new BordermeshDirty { });
                if (EntityManager.HasComponent<ItemsTransformsDirty>(chunkEntity))
                    PostUpdateCommands.AddComponent(chunkEntity, new ItemsTransformsDirty { });
            }
        }
    }
}

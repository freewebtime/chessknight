using Assets.GameCode.Configs;
using Assets.GameCode.Map.Data;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.GameCode.Map.Logic.MeshGeneration
{
    [UpdateInGroup(typeof(GameLoop.PreRendering))]
    public class GroundMeshGeneratorSystem : ComponentSystem
    {
        struct MeshGroup
        {
            public readonly int Length;
            [ReadOnly] public EntityArray entity;
            [ReadOnly] public ComponentDataArray<GroundmeshDirty> dirty;
            [ReadOnly] public ComponentDataArray<MapChunk> chunk;
            [ReadOnly] public SharedComponentDataArray<Groundmesh> mesh;
            [ReadOnly] public SharedComponentDataArray<Heightmap> heightmap;
            [ReadOnly] public SharedComponentDataArray<Groundmap> groundmap;
        }

        [Inject] MeshGroup meshGroup;

        protected override void OnUpdate()
        {
            var mapConfig = ProgramConfig.config.mapConfigs[0];
            var groundConfigs = mapConfig.groundTypes;

            for (int i = 0; i < meshGroup.Length; i++)
            {
                var chunkEntity = meshGroup.entity[i];
                var groundmesh = meshGroup.mesh[i];
                var heightmap = meshGroup.heightmap[i];
                var groundmap = meshGroup.groundmap[i];
                var chunk = meshGroup.chunk[i];

                var chunkSize = chunk.size;
                var chunkCoord = chunk.coordinate;
                var chunkPos = chunk.position;
                var seaHeightScale = 0.9f;

                // dispose old mesh
                if (groundmesh.value != null)
                {
                    Object.Destroy(groundmesh.value);
                }

                var verts = new NativeList<Vector3>(Allocator.Temp);
                var tris = new NativeList<int>(Allocator.Temp);
                var uv = new NativeList<Vector2>(Allocator.Temp);

                for (int y = 0; y < chunkSize.y; y++)
                {
                    for (int x = 0; x < chunkSize.x; x++)
                    {
                        var localCoord = new int2(x, y);
                        var cellCoord = chunkPos + localCoord;
                        var cellCoordOffet = new float3(cellCoord.x, 0, cellCoord.y);
                        var cellIndex = x + y * chunkSize.x;

                        // position
                        var height00 = heightmap.height00[cellIndex];
                        var height01 = heightmap.height01[cellIndex];
                        var height11 = heightmap.height11[cellIndex];
                        var height10 = heightmap.height10[cellIndex];
                        var heightCenter = heightmap.center[cellIndex];

                        var fHeight00 = height00 - 1 + seaHeightScale;
                        var fHeight01 = height01 - 1 + seaHeightScale;
                        var fHeight11 = height11 - 1 + seaHeightScale;
                        var fHeight10 = height10 - 1 + seaHeightScale;

                        var position00 = new float3(0, fHeight00, 0) + cellCoordOffet;
                        var position01 = new float3(0, fHeight01, 1) + cellCoordOffet;
                        var position11 = new float3(1, fHeight11, 1) + cellCoordOffet;
                        var position10 = new float3(1, fHeight10, 0) + cellCoordOffet;
                        var positionCenter = new float3(0.5f, heightCenter, 0.5f) + cellCoordOffet;

                        // triangles
                        for (int j = 0; j < 12; j++)
                        {
                            tris.Add(12 * cellIndex + j);
                        }

                        // vertices
                        // north
                        verts.Add(position01);
                        verts.Add(position11);
                        verts.Add(positionCenter);

                        // east
                        verts.Add(position11);
                        verts.Add(position10);
                        verts.Add(positionCenter);

                        // south
                        verts.Add(position10);
                        verts.Add(position00);
                        verts.Add(positionCenter);

                        // west
                        verts.Add(position00);
                        verts.Add(position01);
                        verts.Add(positionCenter);

                        // uv
                        // north
                        var groundTypeNorth = groundmap.north[cellIndex];
                        var groundConfigNorth = groundConfigs[groundTypeNorth];
                        var uvNorth = groundConfigNorth.uv;

                        uv.Add(uvNorth.uv01);
                        uv.Add(uvNorth.uv11);
                        uv.Add(uvNorth.uv00 + (uvNorth.uv11 - uvNorth.uv00) / 2f);

                        // east
                        var groundTypeEast = groundmap.east[cellIndex];
                        var groundConfigEast = groundConfigs[groundTypeEast];
                        var uvEast = groundConfigEast.uv;

                        uv.Add(uvEast.uv01);
                        uv.Add(uvEast.uv11);
                        uv.Add(uvEast.uv00 + (uvEast.uv11 - uvEast.uv00) / 2f);

                        // south
                        var groundTypeSouth = groundmap.south[cellIndex];
                        var groundConfigSouth = groundConfigs[groundTypeSouth];
                        var uvSouth = groundConfigSouth.uv;

                        uv.Add(uvSouth.uv01);
                        uv.Add(uvSouth.uv11);
                        uv.Add(uvSouth.uv00 + (uvSouth.uv11 - uvSouth.uv00) / 2f);

                        // west
                        var groundTypeWest = groundmap.west[cellIndex];
                        var groundConfigWest = groundConfigs[groundTypeWest];
                        var uvWest = groundConfigWest.uv;

                        uv.Add(uvWest.uv01);
                        uv.Add(uvWest.uv11);
                        uv.Add(uvWest.uv00 + (uvWest.uv11 - uvWest.uv00) / 2f);
                    }
                }

                // create mesh
                var mesh = new Mesh
                {
                    uv = uv.ToArray(),
                    vertices = verts.ToArray(),
                    triangles = tris.ToArray()
                };

                mesh.RecalculateNormals();
                mesh.RecalculateTangents();
                mesh.RecalculateBounds();

                // dispose buffers
                verts.Dispose();
                tris.Dispose();
                uv.Dispose();

                // save data
                PostUpdateCommands.SetSharedComponent(chunkEntity, new Groundmesh
                {
                    value = mesh
                });

                // remove dirty mark
                PostUpdateCommands.RemoveComponent<GroundmeshDirty>(chunkEntity);
            }
        }
    }
}

using Assets.GameCode.Configs;
using Assets.GameCode.Map.Data;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Assets.GameCode.Map.Logic.Rendering
{
    [UpdateInGroup(typeof(GameLoop.Rendering))]
    public class RenderGroundMeshSystem: ComponentSystem
    {
        struct Grounds
        {
            public readonly int Length;
            [ReadOnly] public SharedComponentDataArray<Groundmesh> mesh;
        }

        [Inject] Grounds grounds;

        protected override void OnUpdate()
        {
            var programConfig = ProgramConfig.config;
            var mapConfig = programConfig.mapConfigs[0];
            var material = mapConfig.groundMaterial;
            var matrix = Matrix4x4.identity;

            for (int i = 0; i < grounds.Length; i++)
            {
                var mesh = grounds.mesh[i].value;
                if (mesh == null || mesh.vertexCount <= 0)
                {
                    continue;
                }

                Graphics.DrawMesh(mesh, matrix, material, 0);
            }
        }
    }
}

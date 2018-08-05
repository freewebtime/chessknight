using Assets.GameCode.Map.Data;
using Unity.Collections;
using Unity.Entities;

namespace Assets.GameCode.Map.Logic
{
    public class CleanupWorldMapSystem : ComponentSystem
    {
        struct MapGroup
        {
            public readonly int Length;
            [ReadOnly] public SharedComponentDataArray<Chunksmap> toClenup;
        }
        struct HeightmapGroup
        {
            public readonly int Length;
            [ReadOnly] public SharedComponentDataArray<Heightmap> toClenup;
        }
        struct BordermapGroup
        {
            public readonly int Length;
            [ReadOnly] public SharedComponentDataArray<Bordermap> toClenup;
        }
        struct GroundmapGroup
        {
            public readonly int Length;
            [ReadOnly] public SharedComponentDataArray<Groundmap> toClenup;
        }
        struct ItemsmapGroup
        {
            public readonly int Length;
            [ReadOnly] public SharedComponentDataArray<Itemsmap> toClenup;
        }
        struct ItemsTransformsGroup
        {
            public readonly int Length;
            [ReadOnly] public SharedComponentDataArray<ItemsTransformmap> toClenup;
        }
        struct WatermapGroup
        {
            public readonly int Length;
            [ReadOnly] public SharedComponentDataArray<Watermap> toClenup;
        }

        [Inject] MapGroup mapGroup;
        [Inject] HeightmapGroup heightmapGroup;
        [Inject] BordermapGroup bordermapGroup;
        [Inject] GroundmapGroup groundmapGroup;
        [Inject] ItemsmapGroup itemsmapGroup;
        [Inject] ItemsTransformsGroup transformsGroup;
        [Inject] WatermapGroup watermapGroup;

        protected override void OnDestroyManager()
        {
            for (int i = 0; i < mapGroup.Length; i++)
            {
                if (mapGroup.toClenup[i].value.IsCreated)
                    mapGroup.toClenup[i].value.Dispose();
            }
            for (int i = 0; i < heightmapGroup.Length; i++)
            {
                if (heightmapGroup.toClenup[i].height01.IsCreated)
                    heightmapGroup.toClenup[i].height01.Dispose();
                if (heightmapGroup.toClenup[i].height10.IsCreated)
                    heightmapGroup.toClenup[i].height10.Dispose();
                if (heightmapGroup.toClenup[i].height00.IsCreated)
                    heightmapGroup.toClenup[i].height00.Dispose();
                if (heightmapGroup.toClenup[i].height11.IsCreated)
                    heightmapGroup.toClenup[i].height11.Dispose();
                if (heightmapGroup.toClenup[i].center.IsCreated)
                    heightmapGroup.toClenup[i].center.Dispose();
            }
            for (int i = 0; i < bordermapGroup.Length; i++)
            {
                if (bordermapGroup.toClenup[i].east.IsCreated)
                    bordermapGroup.toClenup[i].east.Dispose();
                if (bordermapGroup.toClenup[i].west.IsCreated)
                    bordermapGroup.toClenup[i].west.Dispose();
                if (bordermapGroup.toClenup[i].north.IsCreated)
                    bordermapGroup.toClenup[i].north.Dispose();
                if (bordermapGroup.toClenup[i].south.IsCreated)
                    bordermapGroup.toClenup[i].south.Dispose();
            }
            for (int i = 0; i < groundmapGroup.Length; i++)
            {
                if (groundmapGroup.toClenup[i].east.IsCreated)
                    groundmapGroup.toClenup[i].east.Dispose();
                if (groundmapGroup.toClenup[i].west.IsCreated)
                    groundmapGroup.toClenup[i].west.Dispose();
                if (groundmapGroup.toClenup[i].north.IsCreated)
                    groundmapGroup.toClenup[i].north.Dispose();
                if (groundmapGroup.toClenup[i].south.IsCreated)
                    groundmapGroup.toClenup[i].south.Dispose();
            }
            for (int i = 0; i < itemsmapGroup.Length; i++)
            {
                if (itemsmapGroup.toClenup[i].value.IsCreated)
                    itemsmapGroup.toClenup[i].value.Dispose();
            }
            for (int i = 0; i < transformsGroup.Length; i++)
            {
                if (transformsGroup.toClenup[i].matrix.IsCreated)
                    transformsGroup.toClenup[i].matrix.Dispose();
                if (transformsGroup.toClenup[i].position.IsCreated)
                    transformsGroup.toClenup[i].position.Dispose();
                if (transformsGroup.toClenup[i].rotation.IsCreated)
                    transformsGroup.toClenup[i].rotation.Dispose();
                if (transformsGroup.toClenup[i].scale.IsCreated)
                    transformsGroup.toClenup[i].scale.Dispose();
            }
            for (int i = 0; i < watermapGroup.Length; i++)
            {
                if (watermapGroup.toClenup[i].height11.IsCreated)
                    watermapGroup.toClenup[i].height11.Dispose();
                if (watermapGroup.toClenup[i].height00.IsCreated)
                    watermapGroup.toClenup[i].height00.Dispose();
                if (watermapGroup.toClenup[i].height01.IsCreated)
                    watermapGroup.toClenup[i].height01.Dispose();
                if (watermapGroup.toClenup[i].height10.IsCreated)
                    watermapGroup.toClenup[i].height10.Dispose();
                if (watermapGroup.toClenup[i].center.IsCreated)
                    watermapGroup.toClenup[i].center.Dispose();
            }

            base.OnDestroyManager();
        }

        protected override void OnUpdate()
        {
        }
    }
}

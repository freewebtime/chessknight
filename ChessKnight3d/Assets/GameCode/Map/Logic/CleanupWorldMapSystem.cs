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
            [ReadOnly] public SharedComponentDataArray<ItemsTransforms> toClenup;
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
                if (heightmapGroup.toClenup[i].east.IsCreated)
                    heightmapGroup.toClenup[i].east.Dispose();
                if (heightmapGroup.toClenup[i].west.IsCreated)
                    heightmapGroup.toClenup[i].west.Dispose();
                if (heightmapGroup.toClenup[i].north.IsCreated)
                    heightmapGroup.toClenup[i].north.Dispose();
                if (heightmapGroup.toClenup[i].south.IsCreated)
                    heightmapGroup.toClenup[i].south.Dispose();
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
                if (itemsmapGroup.toClenup[i].id.IsCreated)
                    itemsmapGroup.toClenup[i].id.Dispose();
                if (itemsmapGroup.toClenup[i].position.IsCreated)
                    itemsmapGroup.toClenup[i].position.Dispose();
                if (itemsmapGroup.toClenup[i].rotation.IsCreated)
                    itemsmapGroup.toClenup[i].rotation.Dispose();
                if (itemsmapGroup.toClenup[i].scale.IsCreated)
                    itemsmapGroup.toClenup[i].scale.Dispose();
            }
            for (int i = 0; i < transformsGroup.Length; i++)
            {
                if (transformsGroup.toClenup[i].value.IsCreated)
                    transformsGroup.toClenup[i].value.Dispose();
            }
            for (int i = 0; i < watermapGroup.Length; i++)
            {
                if (watermapGroup.toClenup[i].east.IsCreated)
                    watermapGroup.toClenup[i].east.Dispose();
                if (watermapGroup.toClenup[i].west.IsCreated)
                    watermapGroup.toClenup[i].west.Dispose();
                if (watermapGroup.toClenup[i].north.IsCreated)
                    watermapGroup.toClenup[i].north.Dispose();
                if (watermapGroup.toClenup[i].south.IsCreated)
                    watermapGroup.toClenup[i].south.Dispose();
            }

            base.OnDestroyManager();
        }

        protected override void OnUpdate()
        {
        }
    }
}

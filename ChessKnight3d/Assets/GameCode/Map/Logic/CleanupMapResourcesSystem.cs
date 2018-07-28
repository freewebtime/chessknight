using Assets.GameCode.Map.Data.Resources;
using Unity.Collections;
using Unity.Entities;

namespace Assets.GameCode.Map.Logic
{
    public class CleanupMapResourcesSystem : ComponentSystem
    {
        struct LibsGroup
        {
            public readonly int Length;
            [ReadOnly] public SharedComponentDataArray<MapResourcesLib> toCleanup;
        }
        struct MapPacksGroup
        {
            public readonly int Length;
            [ReadOnly] public SharedComponentDataArray<MapResourcePack> toCleanup;
        }

        [Inject] LibsGroup libsGroup;
        [Inject] MapPacksGroup mapPacksGroup;

        protected override void OnDestroyManager()
        {
            for (int i = 0; i < libsGroup.Length; i++)
            {
                var toCleanup = libsGroup.toCleanup[i];
                if (toCleanup.mapPacks.IsCreated)
                    toCleanup.mapPacks.Dispose();
            }
            for (int i = 0; i < mapPacksGroup.Length; i++)
            {
                var toCleanup = mapPacksGroup.toCleanup[i];
                if (toCleanup.mapItems.IsCreated)
                    toCleanup.mapItems.Dispose();
                if (toCleanup.groundTypeConfigs.IsCreated)
                    toCleanup.groundTypeConfigs.Dispose();
            }

            base.OnDestroyManager();
        }

        protected override void OnUpdate()
        {
        }
    }
}

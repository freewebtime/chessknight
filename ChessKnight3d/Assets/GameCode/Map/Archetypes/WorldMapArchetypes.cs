using Assets.GameCode.Map.Data;
using Assets.GameCode.Map.Data.Events;
using Unity.Entities;

namespace Assets.GameCode.Map
{
    public static class WorldMapArchetypes
    {
        public static EntityArchetype worldMap;
        public static EntityArchetype mapChunk;

        public static EntityArchetype createMapRequest;
        public static EntityArchetype destroyMapRequest;
        public static EntityArchetype initMapResourcesLibRequest;

        public static EntityArchetype mapResourceLib;
        public static EntityArchetype mapResourcePack;
        public static EntityArchetype mapItemResourcePack;

        public static void Initialize(EntityManager entityManager)
        {
            worldMap = entityManager.CreateArchetype(
                ComponentType.Create<WorldMap>(),
                ComponentType.Create<Chunksmap>(),
                ComponentType.Create<MapResourcesIndex>()
            );

            mapChunk = entityManager.CreateArchetype(
                ComponentType.Create<MapChunk>(),
                ComponentType.Create<Heightmap>(),
                ComponentType.Create<Groundmap>(),
                ComponentType.Create<Bordermap>(),
                ComponentType.Create<Watermap>(),
                ComponentType.Create<Itemsmap>(),
                ComponentType.Create<ItemsTransformmap>(),
                ComponentType.Create<WorldMapRef>(),

                ComponentType.Create<Groundmesh>(),
                ComponentType.Create<Watermesh>(),
                ComponentType.Create<Bordermesh>()
            );

            createMapRequest = entityManager.CreateArchetype(
                ComponentType.Create<CreateMapRequest>()
            );
            destroyMapRequest = entityManager.CreateArchetype(
                ComponentType.Create<DestroyMapRequest>()
            );
        }
    }
}

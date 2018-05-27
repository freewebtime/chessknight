using ChessKnight.Application;
using toinfiniityandbeyond.Rendering2D;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace ChessKnight.GameMatch
{
    public class LoadMatchSystem : ComponentSystem
    {
        public struct RequestsGroup
        {
            [ReadOnly] public ComponentDataArray<LoadMatchRequest> Requests;
            [ReadOnly] public EntityArray Entities;
            public int Length;
        }

        public struct MatchesGroup
        {
            [ReadOnly] public ComponentDataArray<Match> Matches;
            [ReadOnly] public EntityArray Entities;
            public int Length;
        }

        public struct ConfigsGroup
        {
            [ReadOnly] public ComponentDataArray<App> Apps;
            [ReadOnly] public ComponentDataArray<AppConfig> AppConfigs;
            [ReadOnly] public SharedComponentDataArray<AppMediaConfig> MediaConfigs;
            public int Length;
        }

        [Inject] RequestsGroup requestsGroup;
        [Inject] MatchesGroup matchesGroup;
        [Inject] ConfigsGroup configsGroup;

        protected override void OnUpdate()
        {
            if (matchesGroup.Length > 0 || requestsGroup.Length < 1 || configsGroup.Length < 1)
            {
                return;
            }

            var request = requestsGroup.Requests[0];
            var entityRequest = requestsGroup.Entities[0];
            PostUpdateCommands.DestroyEntity(entityRequest);

            var roomSize = request.roomSize;
            var deskSize = request.deskSize;
            var deskOffset = (request.roomSize - request.deskSize) / 2;

            // get configs
            var appConfig = configsGroup.AppConfigs[0];
            var mediaConfig = configsGroup.MediaConfigs[0];

            // create match
            var matchEntity = EntityManager.CreateEntity(appConfig.MatchArchetype);
            // create player
            var matchPlayer = EntityManager.CreateEntity(appConfig.MatchPlayerArchetype);

            // create match room
            var matchRoom = EntityManager.CreateEntity(appConfig.MatchRoomArchetype);
            EntityManager.SetComponentData(matchRoom, new MatchRoom { size = roomSize });

            // create room backgrounds
            for (int y = 0; y < roomSize.y; y++)
            {
                for (int x = 0; x < roomSize.x; x++)
                {
                    // choose sprite
                    var sprite = mediaConfig.MediaConfig.BackgroundSprites[0];

                    var texture = sprite.texture;
                    var pivot = new float2(0.5f, 0.5f);
                    var pixelsPerUnit = sprite.pixelsPerUnit;

                    // create background item
                    var coord = new int2(x, y);
                    var bgItemEntity = EntityManager.CreateEntity(appConfig.MatchDeskItemArchetype);
                    EntityManager.SetComponentData(bgItemEntity, new MatchDeskCoordinate { value = coord });
                    EntityManager.SetComponentData(bgItemEntity, new MatchDeskPosition { value = coord });
                    EntityManager.SetSharedComponentData(bgItemEntity, new SpriteInstanceRenderer
                    {
                        sprite = texture,
                        pivot = pivot,
                        pixelsPerUnit = pixelsPerUnit
                    });
                    EntityManager.SetComponentData(bgItemEntity, new Position { Value = new float3(coord.x, coord.y, 0) });
                }
            }

            // create match desk
            var matchDesk = EntityManager.CreateEntity(appConfig.MatchDeskArchetype);
            EntityManager.SetComponentData(matchDesk, new MatchDesk { size = deskSize, offset = deskOffset });

            // create desk items
            for (int y = 0; y < deskSize.y; y++)
            {
                for (int x = 0; x < deskSize.x; x++)
                {
                    var coord = new int2(x, y) + deskOffset;

                    // choose sprite
                    var spriteIndex = math.abs(x * deskSize.x + y) % (mediaConfig.MediaConfig.BackgroundSprites.Length - 1);
                    spriteIndex = (coord.x % 2 + coord.y % 2) % 2;
                    var sprite = mediaConfig.MediaConfig.BackgroundSprites[spriteIndex + 1];

                    var texture = sprite.texture;
                    var pivot = new float2(0.5f, 0.5f);
                    var pixelsPerUnit = sprite.pixelsPerUnit;

                    // create background item
                    var bgItemEntity = EntityManager.CreateEntity(appConfig.MatchDeskItemArchetype);
                    EntityManager.SetComponentData(bgItemEntity, new MatchDeskCoordinate { value = coord });
                    EntityManager.SetComponentData(bgItemEntity, new MatchDeskPosition { value = coord });
                    EntityManager.SetSharedComponentData(bgItemEntity, new SpriteInstanceRenderer
                    {
                        sprite = texture,
                        pivot = pivot,
                        pixelsPerUnit = pixelsPerUnit
                    });
                    EntityManager.SetComponentData(bgItemEntity, new Position { Value = new float3(coord.x, coord.y, -1) });
                }
            }
        }
    }
}

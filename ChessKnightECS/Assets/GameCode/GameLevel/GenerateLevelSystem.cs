using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace ChessKnight.GameLevel
{
    public struct GenerateLevelRequest: IComponentData
    {
        public int levelId;
        public int2 roomSize;
        public int2 deskSize;
        public int2 deskOffset;
        public int isFirstStepAnywhere;
        public int isStartingOnDesk;

        public int seed;
        public float blockChance;
        public float lockChance;
        public float bombChance;
        public float starChance;
    }

    public class GenerateLevelSystem : ComponentSystem
    {
        struct RequestGroup
        {
            [ReadOnly] public ComponentDataArray<GenerateLevelRequest> Requests;
            [ReadOnly] public EntityArray Entities;
            public int Length;
        }

        [Inject] RequestGroup requestGroup;

        protected override void OnUpdate()
        {
            for (int i = 0; i < requestGroup.Length; i++)
            {
                var request = requestGroup.Requests[i];
                var entity = requestGroup.Entities[i];
                PostUpdateCommands.DestroyEntity(entity);

                var deskSize = request.deskSize;
                var deskOffset = request.deskOffset;
                var roomSize = request.roomSize;
                var isFirstStepAnywhere = request.isFirstStepAnywhere;
                var isStartingOnDesk = request.isStartingOnDesk;
                var levelItems = new List<LevelItemBlueprint>();
                Random.InitState(request.seed);
                var figuresCount = 6;

                // create level items
                // 1. create backgrounds
                for (int y = 0; y < roomSize.y; y++)
                {
                    for (int x = 0; x < roomSize.x; x++)
                    {
                        var coordinate = new int2(x, y);
                        var levelItem = new LevelItemBlueprint
                        {
                            itemType = LevelItemType.Background,
                            version = 0,
                            coordinate = coordinate,
                        };

                        levelItems.Add(levelItem);
                    }
                }

                // 2. create desk
                for (int y = 0; y < deskSize.y; y++)
                {
                    for (int x = 0; x < deskSize.x; x++)
                    {
                        var coordinate = new int2(x, y) + deskOffset;
                        var version = (coordinate.x % 2 + coordinate.y % 2) % 2;
                        var levelItem = new LevelItemBlueprint
                        {
                            itemType = LevelItemType.DeskCell,
                            coordinate = coordinate,
                            version = version
                        };

                        levelItems.Add(levelItem);
                    }
                }

                // 3. create player unit
                var playerCoordinate = new int2(deskOffset.x - 1, deskOffset.y + deskSize.y / 2);
                if (request.isStartingOnDesk > 0)
                {
                    playerCoordinate.x = Random.Range(deskOffset.x, deskOffset.x + deskSize.x);
                    playerCoordinate.y = Random.Range(deskOffset.y, deskOffset.y + deskSize.y);
                }

                var playerUnit = new LevelItemBlueprint
                {
                    coordinate = playerCoordinate,
                    itemType = LevelItemType.PlayerUnit,
                    version = Random.Range(0, figuresCount)
            };
                levelItems.Add(playerUnit);

                // 4. create figures, stars, locks, blocks and bombs
                for (int y = 0; y < deskSize.y; y++)
                {
                    for (int x = 0; x < deskSize.x; x++)
                    {
                        var coordinate = new int2(x, y) + deskOffset;
                        var isPlayerHere = coordinate.Equals(playerCoordinate);

                        // blocks
                        var isBlock = Random.value < request.blockChance;
                        if (isBlock && !isPlayerHere)
                        {
                            var blockItem = new LevelItemBlueprint
                            {
                                coordinate = coordinate,
                                itemType = LevelItemType.Block,
                                version = 0
                            };
                            levelItems.Add(blockItem);
                        }

                        // figure
                        var figure = Random.Range(0, figuresCount);
                        if (!isBlock && !isPlayerHere)
                        {
                            var figureItem = new LevelItemBlueprint
                            {
                                coordinate = coordinate,
                                itemType = LevelItemType.CellFigure,
                                version = figure
                            };
                            levelItems.Add(figureItem);
                        }

                        // locks
                        if (Random.value < request.lockChance && !isBlock && !isPlayerHere)
                        {
                            var lockItem = new LevelItemBlueprint
                            {
                                coordinate = coordinate,
                                itemType = LevelItemType.Lock,
                                version = 0
                            };
                            levelItems.Add(lockItem);
                        }

                        // stars
                        if (Random.value < request.starChance && !isBlock && !isPlayerHere)
                        {
                            var starItem = new LevelItemBlueprint
                            {
                                coordinate = coordinate,
                                itemType = LevelItemType.Star,
                                version = 0
                            };
                            levelItems.Add(starItem);
                        }

                        // bombs
                        if (Random.value < request.bombChance
                            && !isPlayerHere
                            && !isBlock 
                            && figure != (int)ChessFigure.King 
                            && figure != (int)ChessFigure.Pawn
                            )
                        {
                            var lockItem = new LevelItemBlueprint
                            {
                                coordinate = coordinate,
                                itemType = LevelItemType.Bomb,
                                version = 0
                            };
                            levelItems.Add(lockItem);
                        }
                    }
                }

                // 5. create blueprint and put it to a new entity
                var levelBlueprint = new LevelBlueprint
                {
                    deskSize = deskSize,
                    roomSize = roomSize,
                    deskOffset = deskOffset,
                    levelItems = levelItems.ToArray(),
                };
                var blueprintEntity = EntityManager.CreateEntity(
                    ComponentType.Create<LevelBlueprint>(),
                    ComponentType.Create<LevelBlueprintId>()
                    );
                EntityManager.SetSharedComponentData(blueprintEntity, levelBlueprint);
                EntityManager.SetSharedComponentData(blueprintEntity, new LevelBlueprintId { levelId = request.levelId });
            }
        }

        private static int RandomByCoordinate(int2 coordinate, int gridWidth)
        {
            return math.abs(coordinate.y * gridWidth + coordinate.y);
        }
    }
}

using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Ck.Gameplay
{
  public class LevelGenerationApi : ComponentSystem
  {
    [Inject] DataResourcesApi dataResourcesApi;

    public MatchConfig? GenerateRandomMatch(int2 levelSize, uint randomSeed)
    {
      // extract data prefabs from resources
      var deskResources = dataResourcesApi.GetDeskResources();
      if (!deskResources.HasValue) {
        return null;
      }

      // this is our prefabs
      var deskItemsPrefabs = deskResources.Value;

      // desk items
      var armorPrefab = deskItemsPrefabs.Armor;
      var bgLight = deskItemsPrefabs.BackgroundLight;
      var bgDark = deskItemsPrefabs.BackgroundDark;
      var bombPrefab = deskItemsPrefabs.Bomb;
      var goalPrefab = deskItemsPrefabs.Goal;
      var moveTargetPrefab = deskItemsPrefabs.MoveTarget;
      var playerUnit = deskItemsPrefabs.PlayerUnit;

      // chess figure desk items
      var knight = deskItemsPrefabs.Knight;
      var pawn = deskItemsPrefabs.Pawn;
      var rook = deskItemsPrefabs.Rook;
      var bishop = deskItemsPrefabs.Bishop;
      var queen = deskItemsPrefabs.Queen;
      var king = deskItemsPrefabs.King;

      // validation
      if (
        bgLight == null 
        || bgDark == null
        || bombPrefab == null

        || knight == null
        || bishop == null
        || pawn == null
        || rook == null
        || queen == null
        || king == null

        || playerUnit == null

        || goalPrefab == null 
        || armorPrefab == null
        || moveTargetPrefab == null
      ) {
        return null;
      }

      // create level
      var rnd = new Unity.Mathematics.Random(randomSeed);

      var isHiddenChance = 10;
      var bombChance = 40;
      var armorChance = 30;
      var goalChance = 70;

      // TODO: prefabs should be already sorted in some InitDataResources system or so
      var sortedFigures = new GameObject[6];
      sortedFigures[(int)ChessFigureTypes.Bishop] = bishop;
      sortedFigures[(int)ChessFigureTypes.King] = king;
      sortedFigures[(int)ChessFigureTypes.Knight] = knight;
      sortedFigures[(int)ChessFigureTypes.Pawn] = pawn;
      sortedFigures[(int)ChessFigureTypes.Queen] = queen;
      sortedFigures[(int)ChessFigureTypes.Rook] = rook;

      var deskItems = new List<DeskItemConfig>();

      for (int y = 0; y < levelSize.y; y++)
      {
        for (int x = 0; x < levelSize.x; x++)
        {
          var coordinate = new int2(x, y);

          var isHidden = rnd.NextInt(100) < isHiddenChance;
          var isOdd = (x + y) % 2f == 0;
          var figureIndex = rnd.NextInt(sortedFigures.Length);
          var figurePrefab = sortedFigures[figureIndex];
          var isGoal = rnd.NextInt(100) < goalChance;
          var isBomb = rnd.NextInt(100) < bombChance;
          // bomb can't be placed on cell with figure that can move only to adjacent cell
          isBomb = isBomb && figureIndex != (int)ChessFigureTypes.Pawn && figureIndex != (int)ChessFigureTypes.King;
          var isArmor = rnd.NextInt(100) < armorChance;

          // background
          GameObject bgPrefab;
          if (isOdd) {
            bgPrefab = bgLight;
          } else {
            bgPrefab = bgDark;
          }

          deskItems.Add(new DeskItemConfig {
            Coordinate = coordinate,
            Prefab = bgPrefab
          });

          // we don't draw anything if cell is hidden
          if (isHidden) {
            continue;
          }

          // goal
          if (isGoal) {
            deskItems.Add(new DeskItemConfig {
              Coordinate = coordinate,
              Prefab = goalPrefab
            });
          }

          // figure
          if (!isHidden) {
            deskItems.Add(new DeskItemConfig {
              Coordinate = coordinate,
              Prefab = figurePrefab
            });
          }

          // armor
          if (!isHidden && isArmor) {
            deskItems.Add(new DeskItemConfig {
              Coordinate = coordinate,
              Prefab = armorPrefab
            });
          }

          // move target
          if (!isHidden) {
            deskItems.Add(new DeskItemConfig {
              Coordinate = coordinate,
              Prefab = moveTargetPrefab
            });
          }

          // bomb
          if (!isHidden && isBomb) {
            deskItems.Add(new DeskItemConfig {
              Coordinate = coordinate,
              Prefab = bombPrefab
            });
          }
          
        }
      }

      var deskConfig = new DeskConfig {
        DeskItems = deskItems.ToArray()
      };
      var result = new MatchConfig {
        DeskConfig = deskConfig
      };

      return result;
    }

    public MatchConfig? GenerateClassicChessKnightMatch()
    {
      return default;
    }

    protected override void OnUpdate() {}
  }
}
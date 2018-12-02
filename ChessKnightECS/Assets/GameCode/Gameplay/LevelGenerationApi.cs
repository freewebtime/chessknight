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

      var deskDataSkin = deskResources.Value.DefaultDesk;

      // prepare desk items prefabs

      // armor
      GameObject armorPrefab = null;
      if (deskDataSkin.Armor != null && deskDataSkin.Armor.Length > 0)
      {
        armorPrefab = deskDataSkin.Armor[0];
      }

      // background
      GameObject bgLight = null;
      GameObject bgDark = null;

      if (deskDataSkin.Background != null && deskDataSkin.Background.Length > 0)
      {
        bgLight = deskDataSkin.Background[0];
        if (deskDataSkin.Background.Length > 1) {
          bgDark = deskDataSkin.Background[1];
        }
        else
        {
          bgDark = bgLight;
        }
      }

      // bomb
      GameObject bombPrefab = null;
      if (deskDataSkin.Bomb != null && deskDataSkin.Bomb.Length > 0)
      {
        bombPrefab = deskDataSkin.Bomb[0];
      }

      // figure
      GameObject[] figurePrefabs = null;
      if (deskDataSkin.Figure != null && deskDataSkin.Figure.Length > 0)
      {
        figurePrefabs = deskDataSkin.Figure;
      }

      // goal
      GameObject goalPrefab = null;
      if (deskDataSkin.Goal != null && deskDataSkin.Goal.Length > 0)
      {
        goalPrefab = deskDataSkin.Goal[0];
      }

      // move target
      GameObject moveTargetPrefab = null;
      if (deskDataSkin.MoveTarget != null && deskDataSkin.MoveTarget.Length > 0)
      {
        moveTargetPrefab = deskDataSkin.MoveTarget[0];
      }

      // player unit
      GameObject[] playerUnitPrefabs = null;
      if (deskDataSkin.PlayerUnit != null && deskDataSkin.PlayerUnit.Length > 0)
      {
        playerUnitPrefabs = deskDataSkin.PlayerUnit;
      }

      // highlight
      GameObject[] highlightPrefabs = null;
      if (deskDataSkin.Highlight != null && deskDataSkin.Highlight.Length > 0)
      {
        highlightPrefabs = deskDataSkin.Highlight;
      }


      // create level
      var rnd = new Unity.Mathematics.Random(randomSeed);

      var isHiddenChance = 10;
      var bombChance = 40;
      var armorChance = 30;
      var goalChance = 70;

      var deskItems = new List<DeskItemConfig>();

      for (int y = 0; y < levelSize.y; y++)
      {
        for (int x = 0; x < levelSize.x; x++)
        {
          var coordinate = new int2(x, y);

          var isHidden = rnd.NextInt(100) < isHiddenChance;
          var isOdd = (x + y) % 2f == 0;
          var chessFigure = rnd.NextInt(6);
          var isGoal = rnd.NextInt(100) < goalChance;
          var isBomb = rnd.NextInt(100) < bombChance;
          // bomb can't be placed on cell with figure that can move only to adjacent cell
          isBomb = isBomb && chessFigure != (int)ChessFigureTypes.Pawn && chessFigure != (int)ChessFigureTypes.King;
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
          if (!isHidden && figurePrefabs.Length > chessFigure) {
            GameObject figurePrefab = figurePrefabs[chessFigure];
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
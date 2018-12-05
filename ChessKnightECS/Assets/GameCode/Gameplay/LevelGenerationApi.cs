using System.Collections.Generic;
using Ck.Resources;
using Fwt.Core.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Ck.Gameplay
{
  public class LevelGenerationApi : ComponentSystem
  {
    [Inject] GameResourcesApi gameResourcesApi;

    public MatchConfig? GenerateRandomMatch(int2 levelSize, uint randomSeed)
    {
      // prepare figure types that play
      var figureVersions = new ChessFigureTypes[] {
        ChessFigureTypes.Pawn,
        ChessFigureTypes.Bishop,
        ChessFigureTypes.Rook,
        ChessFigureTypes.Knight,
        ChessFigureTypes.Queen,
        ChessFigureTypes.King
      };

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
          var figureType = figureVersions.GetRandom();
          var isGoal = rnd.NextInt(100) < goalChance;
          var isBomb = rnd.NextInt(100) < bombChance;
          // bomb can't be placed on cell with figure that can move only to adjacent cell
          isBomb = isBomb && figureType != (int)ChessFigureTypes.Pawn && figureType != ChessFigureTypes.King;
          var isArmor = rnd.NextInt(100) < armorChance;

          // background

          deskItems.Add(new DeskItemConfig {
            Coordinate = coordinate,
            DeskItemType = (int)DeskItemTypes.Background,
            DeskItemVersion = (x + y) % 2
          });

          // we don't draw anything if cell is hidden
          if (isHidden) {
            continue;
          }

          // goal
          if (isGoal) {
            deskItems.Add(new DeskItemConfig {
              Coordinate = coordinate,
              DeskItemType = (int)DeskItemTypes.Goal,
              DeskItemVersion = 0
            });
          }

          // figure
          if (!isHidden) {
            deskItems.Add(new DeskItemConfig {
              Coordinate = coordinate,
              DeskItemType = (int)DeskItemTypes.Figure,
              DeskItemVersion = (int)figureType
            });
          }

          // armor
          if (!isHidden && isArmor) {
            deskItems.Add(new DeskItemConfig {
              Coordinate = coordinate,
              DeskItemType = (int)DeskItemTypes.Armor,
              DeskItemVersion = 0
            });
          }

          // move target
          if (!isHidden) {
            deskItems.Add(new DeskItemConfig {
              Coordinate = coordinate,
              DeskItemType = (int)DeskItemTypes.MoveTarget,
              DeskItemVersion = 0
            });
          }

          // bomb
          if (!isHidden && isBomb) {
            deskItems.Add(new DeskItemConfig {
              Coordinate = coordinate,
              DeskItemType = (int)DeskItemTypes.Bomb,
              DeskItemVersion = 0
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
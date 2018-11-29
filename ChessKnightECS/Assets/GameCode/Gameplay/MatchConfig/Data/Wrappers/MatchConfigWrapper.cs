using System;
using System.Collections.Generic;
using Fwt.Core;
using Fwt.Core.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Ck.Gameplay
{
  [Serializable]
  public struct FigureSkin
  {
    public ChessFigureTypes FigureType;
    public GameObject[] Variants;
  }

  [Serializable]
  public struct MatchDeskSkin
  {
    public GameObject[] Backgrounds;
    public GameObject[] Bombs;
    public FigureSkin[] Figures;
    public GameObject[] Goals;
    public GameObject[] Locks;
    public GameObject[] MoveTargets;
  }

  [Serializable]
  public struct MatchSkinConfig: ISharedComponentData
  {
    public GameObject RoomBackPrefab;
    
    public MatchDeskSkin DeskSkin;
  }

  [CreateAssetMenu]
  public class MatchConfigWrapper: ScriptableObjectWrapper<MatchConfig> 
  {
    public MatchSkinConfigWrapper MatchSkin;

    [ContextMenu("Generate classic 10x10 Knight puzzle")]
    public void Generate10x10Classic()
    {
      if (!MatchSkin) {
        Debug.LogError("Can't generate level. No Match Skin Config");
        return;
      }

      var matchSkinConfig = MatchSkin.value;
      var deskSkinConfig = matchSkinConfig.DeskSkin;

      if (deskSkinConfig.Backgrounds == null || deskSkinConfig.Backgrounds.Length == 0)
      {
        Debug.LogError("Can't generate level. Desk skin data is invalid. No backgrounds");
        return;
      }

      if (deskSkinConfig.Figures == null || deskSkinConfig.Figures.Length == 0)
      {
        Debug.LogError("Can't generate level. Desk skin data is invalid. No figures");
        return;
      }

      if (deskSkinConfig.Goals == null || deskSkinConfig.Goals.Length == 0)
      {
        Debug.LogError("Can't generate level. Desk skin data is invalid. No goals");
        return;
      }

      if (deskSkinConfig.Goals == null || deskSkinConfig.MoveTargets.Length == 0)
      {
        Debug.LogError("Can't generate level. Desk skin data is invalid. No move targets");
        return;
      }

      var backgrounds = deskSkinConfig.Backgrounds;
      var moveTargets = deskSkinConfig.MoveTargets;
      var figures = deskSkinConfig.Figures;
      var goals = deskSkinConfig.Goals;

      var sortedSkins = new Dictionary<ChessFigureTypes, FigureSkin>();
      for (int i = 0; i < figures.Length; i++)
      {
        var figure = figures[i];
        sortedSkins[figure.FigureType] = figure;
      }
      FigureSkin knightSkin;
      if (!sortedSkins.TryGetValue(ChessFigureTypes.Knight, out knightSkin)) {
        Debug.LogError("Can't generate level. Desk skin data is invalid. No knight figure skins");
        return;
      }

      var gridSize = new int2(10, 10);

      var matchConfig = this.value;
      var deskConfig = matchConfig.Desk;
      var deskItems = new List<DeskItemConfig>();

      for (int y = 0; y < gridSize.y; y++)
      {
        for (int x = 0; x < gridSize.x; x++)
        {
          var coordinate = new int2(x, y);

          // move target
          deskItems.Add(new DeskItemConfig {
            Coordinate = new int2(x, y),
            Prefab = moveTargets.GetRandom()
          });

          // figure
          deskItems.Add(new DeskItemConfig {
            Coordinate = new int2(x, y),
            Prefab = knightSkin.Variants.GetRandom()
          });

          // goal
          deskItems.Add(new DeskItemConfig {
            Coordinate = new int2(x, y),
            Prefab = goals.GetRandom()
          });
        }
      }

      deskConfig.DeskItems = deskItems.ToArray();
      matchConfig.Desk = deskConfig;
      this.value = matchConfig;
    }
  }
}
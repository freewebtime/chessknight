using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Ck.Gameplay
{
  
  public struct Level: IComponentData {}

  public class LevelComponent: ComponentDataWrapper<Level> {}


  [Serializable]
  public struct LevelSize: IComponentData
  {
    public int2 Value;
  }

  public class LevelSizeComponent: ComponentDataWrapper<LevelSize> {}

  [Serializable]
  public struct LevelGrid
  {
  }

  public static class LevelGridLayers
  {
    public struct Background
    {
      public int Id;
    }

    public struct Bomb
    {
      public int Id;
    }

    public struct Figure
    {
      public ChessFigureTypes Types;
    }
  }



}
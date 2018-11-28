using Unity.Entities;

namespace Ck.Gameplay
{

  public partial class DeskLayers
  {

    // Cell Background
    public struct Background
    {
      public Entity Value;
    }

    // Move Target
    public struct MoveTarget
    {
      public Entity Value;
    }

    // Figure
    public struct Figure
    {
      public Entity Value;
    }

    // Lock (Inactive)
    public struct Lock
    {
      public Entity Value;
    }

    // Goal
    public struct Goal
    {
      public Entity Value;
    }

    // Bomb
    public struct Bomb
    {
      public Entity Value;
    }

  }

}
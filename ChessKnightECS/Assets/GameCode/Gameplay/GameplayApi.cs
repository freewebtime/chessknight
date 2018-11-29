using Unity.Collections;
using Unity.Entities;

namespace Ck.Gameplay
{
  public class GameplayApi : ComponentSystem
  {
    struct GamestateGroup
    {
      public readonly int Length;
      [ReadOnly] public EntityArray Entity;
      [ReadOnly] public ComponentDataArray<Gamestate> Gamestate;
    }

    [Inject] GamestateGroup gamestateGroup;
    [Inject] ResourcesApi resourcesApi;

    public Gamestate? GetGamestate()
    {
      if (gamestateGroup.Length == 0)
      {
        return null;
      }

      return gamestateGroup.Gamestate[0];
    }

    public void DestroyGamestate()
    {
      for (int i = 0; i < gamestateGroup.Length; i++)
      {
        EntityManager.DestroyEntity(gamestateGroup.Entity[i]);
      }
    }

    public void CreateGamestate()
    {

    }

    protected override void OnCreateManager()
    {
    }

    protected override void OnUpdate() {}
  }
}
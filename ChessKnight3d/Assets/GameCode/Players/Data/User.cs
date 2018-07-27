using Unity.Entities;

namespace Assets.GameCode.Players.Data
{
    public struct User : IComponentData { }

    public struct Player : IComponentData
    {
        public int index;
    }

    public struct PlayerSharedData : ISharedComponentData
    {
        public string Name;
    }
}

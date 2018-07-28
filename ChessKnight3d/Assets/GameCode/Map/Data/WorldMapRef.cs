using Unity.Entities;

namespace Assets.GameCode.Map.Data
{
    public struct WorldMapRef: IComponentData
    {
        public Entity target;
    }
}

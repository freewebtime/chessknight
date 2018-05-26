using Unity.Entities;

namespace Assets.Sources.Components
{
    public struct User: ISharedComponentData
    {
        public int Index;
        public string Name;
    }
}

using Unity.Entities;

namespace Assets.GameCode.App.Data
{
    public struct AppState: IComponentData { }

    public enum SceneTypes
    {
        Nothing,
        MainMenu,
        Settings,
        Levelboard,
        Credits,
        Game,
    }
}

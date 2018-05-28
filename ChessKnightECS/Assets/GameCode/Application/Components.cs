using Unity.Entities;

namespace ChessKnight.Application
{
    public struct App: IComponentData
    {
    }

    public struct AppConfig: IComponentData
    {
        public EntityArchetype MatchArchetype;
        public EntityArchetype MatchPlayerArchetype;
        public EntityArchetype MatchRoomArchetype;
        public EntityArchetype MatchDeskArchetype;
        public EntityArchetype MatchDeskItemArchetype;

        public EntityArchetype LevelArchetype;
        public EntityArchetype LevelItemArchetype;
    }

    public struct AppMediaConfig: ISharedComponentData
    {
        public MediaConfig MediaConfig;
    }

}

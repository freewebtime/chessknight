using Unity.Entities;
using Unity.Mathematics;

namespace ChessKnight.GameMatch
{
    public struct LoadMatchRequest: IComponentData
    {
        public int2 deskSize;
        public int2 roomSize;
    }

    public struct Match: IComponentData
    {
        public MatchStates State;
    }

    public struct MatchPlayer: IComponentData
    {
        public int Index;
    }

    public struct MatchRoom: IComponentData
    {
        public int2 size;
    }

    public struct MatchDesk: IComponentData
    {
        public int2 offset;
        public int2 size;
    }

    public struct MatchDeskItem: IComponentData
    {
    }

    public struct MatchDeskCoordinate: IComponentData
    {
        public int2 value;
    }

    public struct MatchDeskPosition: IComponentData
    {
        public float2 value;
    }

    public struct MatchDeskIndex: IComponentData
    {
        public int Index;
    }

    public struct PlayerInput: IComponentData
    {
        public bool IsActive;
    }

    public enum MatchStates
    {
        Loading,
        ReadyToPlay,
        Playing,
        ReadyToUnload,
        Unloading
    }
}

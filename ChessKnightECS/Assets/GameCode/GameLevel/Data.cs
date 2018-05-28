using System;
using Unity.Entities;
using Unity.Mathematics;

namespace ChessKnight.GameLevel
{
    public struct Level: IComponentData
    {
        public int Id;
        public int2 roomSize;
        public int2 deskSize;
        public int2 deskOffset;
    }

    public enum LevelItemType
    {
        Background = 0,
        DeskCell = 1,
        CellFigure = 2,
        Lock = 3,
        Star = 4,
        Bomb = 5,
        Block = 6,
        PlayerUnit = 7
    }

    public struct GridCoordinate: ISharedComponentData
    {
        public int2 coordinate;
    }

    [Serializable]
    public struct LevelItemBlueprint
    {
        public int2 coordinate;
        public LevelItemType itemType;
        public int version;
    }

    public struct LevelBlueprintId: ISharedComponentData
    {
        public int levelId;
    }

    public struct LevelBlueprint: ISharedComponentData
    {
        public int2 roomSize;
        public int2 deskSize;
        public int2 deskOffset;
        public LevelItemBlueprint[] levelItems;
    }

    public struct BackgroundItem: IComponentData
    {
    }

    public struct FigureItem: IComponentData
    {
        public ChessFigure Figure;
    }

    public struct StarItem: IComponentData
    {
    }

    public struct BlockItem: IComponentData
    {
    }

    public struct LockItem: IComponentData
    {
    }

    public struct BombItem: IComponentData
    {
    }

    public struct PlayerItem: IComponentData
    {
    }

    public struct LevelItem: IComponentData
    {
        public LevelItemType ItemType;
        public int Version;
    }

    public enum ChessFigure
    {
        Pawn = 0,
        Rook = 1,
        Knight = 2,
        Bishop = 3,
        Queen = 4,
        King = 5
    }
}

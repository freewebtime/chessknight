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
        Background,
        DeskCell,
        CellFigure,
        PlayerUnit,
        Star,
        Lock,
        Bomb,
        Block
    }

    public struct LevelItemBlueprint
    {
        public int2 coordinate;
        public LevelItemType itemType;
        public int version;
    }

    public struct LevelBlueprint: ISharedComponentData
    {
        public int levelId;
        public int2 roomSize;
        public int2 deskSize;
        public int2 deskOffset;
        public LevelItemBlueprint[] levelItems;
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
        Bishop = 2,
        Knight = 3,
        Queen = 4,
        King = 5
    }
}

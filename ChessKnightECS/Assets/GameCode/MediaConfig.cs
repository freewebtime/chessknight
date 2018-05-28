using ChessKnight.GameLevel;
using System;
using UnityEngine;

namespace ChessKnight
{
    [CreateAssetMenu]
    public class MediaConfig: ScriptableObject
    {
        public Sprite[] BackgroundSprites;

        public ItemTypeSprites[] ItemTypeSprites;

        [ContextMenu("Sort item type sprites")]
        public void SortItemTypeSprites()
        {
            var newSprites = new ItemTypeSprites[ItemTypeSprites.Length];
            for (int i = 0; i < ItemTypeSprites.Length; i++)
            {
                var sprites = ItemTypeSprites[i];
                newSprites[(int)sprites.ItemType] = sprites;
            }

            ItemTypeSprites = newSprites;
        }
    }

    [Serializable]
    public struct ItemTypeSprites
    {
        public LevelItemType ItemType;
        public Sprite[] Sprites;
        public Vector2 Offset;
    }
}

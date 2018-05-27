using ChessKnight.Gameplay;
using UnityEngine;

namespace ChessKnight
{
    [CreateAssetMenu]
    public class MediaConfig: ScriptableObject
    {
        public Sprite[] BackgroundSprites;

        [Space]
        public GameDeskComponent DeskPrefab;
        public GameDeskItemComponent DeskItemPrefab;

    }
}

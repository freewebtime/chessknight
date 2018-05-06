using UnityEngine;

namespace ChessKnight.Unity
{
    [CreateAssetMenu]
    public class ChessFigureConfig: ScriptableObject
    {
        public ChessFigure Figure;
        public Sprite Sprite;
    }
}

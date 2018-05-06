using UnityEngine;

namespace ChessKnight.Unity.AssetsManagament
{
    [CreateAssetMenu]
    public class GraphicsPackConfig: ScriptableObject
    {
        public ChessFigureConfig[] ChessFigures;

        public Transform[] Backgrounds;
    }
}

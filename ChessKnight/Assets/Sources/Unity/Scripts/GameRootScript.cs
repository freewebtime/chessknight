using System;
using UnityEngine;

namespace ChessKnight.Unity
{
    public class GameRootScript: MonoBehaviour
    {
        public Transform[] Layers;

        public Transform GetLayer(string layerName)
        {
            return Array.Find(Layers, tr => tr.name == layerName);
        }
    }
}

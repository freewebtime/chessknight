using Entitas;
using UnityEngine;

namespace ChessKnight.View
{
    [Game]
    public class ViewRotationComponent : IComponent
    {
        public Quaternion Value;
    }
}

using Entitas;
using UnityEngine;

namespace ChessKnight.View
{
    [Game]
    public class ParentTransformComponent: IComponent
    {
        public Transform Value;
    }
}

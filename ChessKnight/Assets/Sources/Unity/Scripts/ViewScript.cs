using UnityEngine;

namespace ChessKnight.Unity
{
    public class ViewScript: MonoBehaviour
    {
        public Transform Transform;

        protected void OnValidate()
        {
            Transform = transform;
        }
    }
}

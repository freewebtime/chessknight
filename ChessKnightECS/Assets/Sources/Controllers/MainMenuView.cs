using Unity.Entities;
using UnityEngine;

namespace ChessKnight
{
    public class MainMenuView : MonoBehaviour
    {
        public void PlayGame()
        {
            Debug.Log("Play Game clicked");
        }

        public void OpenEditor()
        {
            Debug.Log("Open Editor clicked");
        }

        public void ExitGame()
        {
            Debug.Log("Exit Game clicked");
        }
    }
}
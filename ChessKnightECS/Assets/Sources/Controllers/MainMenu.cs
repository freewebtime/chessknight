using Unity.Entities;
using UnityEngine;

namespace ChessKnight.Controllers
{
    public class MainMenu : MonoBehaviour
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
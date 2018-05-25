using ChessKnight.Unity.AssetsManagament;
using Entitas;
using UnityEngine;

namespace ChessKnight.Unity
{
    public class StartupScript : MonoBehaviour
    {

        public AssetsManagerScript AssetsManager;
        public GameRootScript GameRoot;

        Systems systems;
#if false
        void Start()
        {
            // get a reference to the contexts
            var contexts = Contexts.sharedInstance;

            PrepareContexts(contexts);

            OnStartup(contexts);

            // create the systems by creating individual features
            systems = new Feature("Systems")
                .Add(new ViewFeature(contexts))
                ;

            // call Initialize() on all of the IInitializeSystems
            systems.Initialize();
        }

        private void PrepareContexts(Contexts contexts)
        {
            contexts.game.SetGameRoot(GameRoot);
            contexts.game.SetGraphicsPack(AssetsManager.DefaultGraphics);
        }

        private void OnStartup(Contexts contexts)
        {
        }

        void Update()
        {
            // call Execute() on all the IExecuteSystems and 
            // ReactiveSystems that were triggered last frame
            systems.Execute();
            // call cleanup() on all the ICleanupSystems
            systems.Cleanup();
        }
#endif
    }
}


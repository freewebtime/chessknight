using Entitas;
using UnityEngine;

public class Startup : MonoBehaviour {

    Systems systems;

    void Start()
    {
        // get a reference to the contexts
        var contexts = Contexts.sharedInstance;

        // create the systems by creating individual features
        systems = new Feature("Systems")
            .Add(new DebugSystemsFeature(contexts));

        // call Initialize() on all of the IInitializeSystems
        systems.Initialize();
    }

    void Update()
    {
        // call Execute() on all the IExecuteSystems and 
        // ReactiveSystems that were triggered last frame
        systems.Execute();
        // call cleanup() on all the ICleanupSystems
        systems.Cleanup();
    }
}

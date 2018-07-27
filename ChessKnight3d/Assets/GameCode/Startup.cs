using Assets.GameCode.UiSystem.Data;
using Unity.Entities;
using UnityEngine;

namespace Assets.GameCode
{
    public class Startup: MonoBehaviour
    {
        // game has been started
        protected void Start()
        {
        }

        [ContextMenu("Show hello world confirmation")]
        void ShowHelloWorldConfirmation()
        {
            var entityManager = World.Active.GetOrCreateManager<EntityManager>();

            var requestEntity = entityManager.CreateEntity();
            entityManager.AddSharedComponentData(requestEntity, new ShowConfirmationDialogRequest
            {
                message = "Hello world!. Ok or Cancel?"
            });
        }
    }
}

using UnityEngine;

namespace Assets.GameCode.UiSystem.Components.Controllers
{
    [RequireComponent(typeof(UiRootComponent))]
    public class UiRootController: MonoBehaviour
    {
        public UiScreenComponent[] uiScreenPrefabs;
        public UiScreenComponent[] uiScreens;

        public void Initialize()
        {
            InitializeScreens();
        }

        protected void InitializeScreens()
        {
            uiScreens = new UiScreenComponent[uiScreenPrefabs.Length];
            for (int i = 0; i < uiScreenPrefabs.Length; i++)
            {
                var prefab = uiScreenPrefabs[i];
                var instance = Instantiate(prefab);
                var canvas = instance.GetComponent<Canvas>();
                if (canvas != null)
                {
                    canvas.enabled = false;
                }

                instance.transform.SetParent(transform);

                uiScreens[i] = instance;
            }
        }
    }
}

/*
    qwertyuiopasdfghjkl;'[]zxcvbnm,./`1234567890-=QWERTYUIOP{}ASDFGHJKL:"|ZXCVBNM<>?
 */

//using System;
//using ChessKnight.Components;
//using Unity.Collections;
//using Unity.Entities;
//using ChessKnight.Controllers;

//namespace Assets.Sources.Systems
//{
//    public class MainMenuSystem : ComponentSystem
//    {
//        struct GameAppEntity
//        {
//            public EntityArray Entities;
//            [ReadOnly] public ComponentDataArray<GameApp> GameApp;
//            public int Length;
//        }

//        struct MmUi
//        {
//            public EntityArray Entities;
//            [ReadOnly] public ComponentArray<MainMenuUI> MainMenus;
//            public int Length;
//        }

//        [Inject] private GameAppEntity GameApps;
//        [Inject] private MmUi MainMenuUi;

//        protected override void OnUpdate()
//        {
//            var gameApp = GameApps.GameApp[0];
//            var isMainMenu = gameApp.ActiveScene == GameScenes.MainMenu;

//            if (isMainMenu)
//            {
//                if (MainMenuUi.Length == 0)
//                {

//                }
//            }
//        }
//    }
//}

//using ChessKnight.Components;
//using Unity.Collections;
//using Unity.Entities;
//using UnityEngine;

//namespace Assets.Sources.Systems
//{
//    public class UserInputSystem : ComponentSystem
//    {
//        struct UserEntity
//        {
//            public EntityArray Entities;
//            public ComponentDataArray<InputListener> Listener;
//            [ReadOnly] public SharedComponentDataArray<User> Users;
//            public int Length;
//        }

//        [Inject] private UserEntity Users;

//        protected override void OnUpdate()
//        {
//            for (int i = 0; i < Users.Length; i++)
//            {
//                Debug.Log("Here is an entity " + i);
//            }
//        }
//    }
//}

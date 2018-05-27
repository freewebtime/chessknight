using System;
using Unity.Entities;

namespace ChessKnight.Gameplay
{
    [Serializable]
    public struct GameDeskLayer: IComponentData
    {
        public int Index;
    }

    public class GameDeskLayerComponent: ComponentDataWrapper<GameDeskLayer> { }
}

using System.Collections.Generic;
using Entitas;

namespace ChessKnight.View.Systems
{
    public class SetViewSystem : ReactiveSystem<GameEntity>
    {
        public SetViewSystem(IContext<GameEntity> context) : base(context)
        {
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var view = UnityEngine.Object.Instantiate(entity.viewPrefab.Value);
                entity.ReplaceView(view, entity.viewPrefab.Value);
            }
        }

        protected override bool Filter(GameEntity entity)
        {
            if (!entity.hasViewPrefab || !entity.viewPrefab.Value)
                return false;

            if (!entity.hasView)
                return true;

            if (entity.view.Prefab != entity.viewPrefab.Value)
                return true;

            return false;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.ViewPrefab);
        }
    }
}

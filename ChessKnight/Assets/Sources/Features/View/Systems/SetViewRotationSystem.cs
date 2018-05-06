using System.Collections.Generic;
using Entitas;

namespace ChessKnight.View.Systems
{
    public class SetViewRotationSystem : ReactiveSystem<GameEntity>
    {
        public SetViewRotationSystem(IContext<GameEntity> context) : base(context)
        {
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var view = entity.view.Value;
                var rotation = entity.viewRotation.Value;

                view.Transform.localRotation = rotation;
            }
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.view.Value;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AllOf(GameMatcher.View, GameMatcher.ViewRotation));
        }
    }
}

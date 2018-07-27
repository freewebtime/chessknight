using Unity.Entities;
using UnityEngine.Experimental.PlayerLoop;

namespace Assets.GameCode
{
    public static class GameLoop
    {
        [UpdateAfter(typeof(PostLateUpdate))]
        public class PreCollectingInput : BarrierSystem { }
        [UpdateAfter(typeof(PreCollectingInput))]
        public class CollectingInput : BarrierSystem { }
        [UpdateAfter(typeof(CollectingInput))]
        public class PostCollectingInput : BarrierSystem { }

        [UpdateAfter(typeof(PostCollectingInput))]
        public class PreProcessingInput : BarrierSystem { }
        [UpdateAfter(typeof(PreProcessingInput))]
        public class ProcessingInput : BarrierSystem { }
        [UpdateAfter(typeof(ProcessingInput))]
        public class PostProcessingInput : BarrierSystem { }

        [UpdateAfter(typeof(PostProcessingInput))]
        public class PreProcessingData : BarrierSystem { }
        [UpdateAfter(typeof(PreProcessingData))]
        public class ProcessingData : BarrierSystem { }
        [UpdateAfter(typeof(ProcessingData))]
        public class PostProcessingData : BarrierSystem { }

        [UpdateAfter(typeof(PostProcessingData))]
        public class PreRendering : BarrierSystem { }
        [UpdateAfter(typeof(PreRendering))]
        public class Rendering : BarrierSystem { }
        [UpdateAfter(typeof(Rendering))]
        public class PostRendering : BarrierSystem { }

        [UpdateAfter(typeof(PostRendering))]
        public class PreCleanup : BarrierSystem { }
        [UpdateAfter(typeof(PreCleanup))]
        public class Cleanup : BarrierSystem { }
        [UpdateAfter(typeof(Cleanup))]
        public class PostCleanup : BarrierSystem { }
    }
}

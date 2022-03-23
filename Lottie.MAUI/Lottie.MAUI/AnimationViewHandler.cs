using Microsoft.Maui.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottie.MAUI
{
#if ANDROID
    public partial class AnimationViewHandler
    {
        public static PropertyMapper<IAnimationView, AnimationViewHandler> CustomEntryMapper = new PropertyMapper<IAnimationView, AnimationViewHandler>(ViewHandler.ViewMapper)
        {
            [nameof(IAnimationView.Animation)] = MapAnimation,
            [nameof(IAnimationView.AnimationSource)] = MapAnimationSource,
            [nameof(IAnimationView.CacheComposition)] = MapCacheComposition,
            [nameof(IAnimationView.FallbackResource)] = MapFallbackResource,

            //WARNING: These properties fail rendering
            //[nameof(IAnimationView.MinFrame)] = MapMinFrame,
            //[nameof(IAnimationView.MinProgress)] = MapMinProgress,
            //[nameof(IAnimationView.MaxFrame)] = MapMaxFrame,
            //[nameof(IAnimationView.MaxProgress)] = MapMaxProgress,

            [nameof(IAnimationView.Speed)] = MapSpeed,
            [nameof(IAnimationView.RepeatMode)] = MapRepeatMode,
            [nameof(IAnimationView.RepeatCount)] = MapRepeatCount,
            [nameof(IAnimationView.IsAnimating)] = MapIsAnimating,
            [nameof(IAnimationView.ImageAssetsFolder)] = MapImageAssetsFolder,
            [nameof(IAnimationView.Progress)] = MapProgress,
            [nameof(IAnimationView.Duration)] = MapDuration,
            [nameof(IAnimationView.AutoPlay)] = MapAutoPlay,
            [nameof(IAnimationView.Command)] = MapCommand,
            [nameof(IAnimationView.EnableMergePathsForKitKatAndAbove)] = MapEnableMergePathsForKitKatAndAbove
        };

   
        public AnimationViewHandler() : base(CustomEntryMapper)
        {

        }

        public AnimationViewHandler(PropertyMapper? mapper = null) : base(mapper ?? CustomEntryMapper)
        {

        }
    }
#endif
}

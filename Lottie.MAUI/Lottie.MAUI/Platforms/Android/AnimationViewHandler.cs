#if ANDROID
using Microsoft.Maui.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Airbnb.Lottie;
using Lottie.MAUI;
using Lottie.MAUI.Platforms.Android;
using Android.Graphics;

namespace Lottie.MAUI
{

    public partial class AnimationViewHandler : ViewHandler<IAnimationView, LottieAnimationView>
    {
        public LottieAnimationView NativeView { get; private set; }

        private AnimatorListener _animatorListener;
        private AnimatorUpdateListener _animatorUpdateListener;
        private LottieOnCompositionLoadedListener _lottieOnCompositionLoadedListener;
        private LottieFailureListener _lottieFailureListener;
        private ClickListener _clickListener;

        public AnimationViewHandler(IPropertyMapper mapper, CommandMapper commandMapper = null) 
            : base(mapper, commandMapper)
        {
        }

        #region ViewHandler 

        private static void MapAnimation(AnimationViewHandler viewHandler, IAnimationView animView)
        {
            viewHandler.NativeView.TrySetAnimation((AnimationView)animView);

            if (animView.AutoPlay || animView.IsAnimating)
                viewHandler.NativeView.PlayAnimation();
        }

        private static void MapAnimationSource(AnimationViewHandler viewHandler, IAnimationView animView)
        {
            // Empty
        }
        private static void MapCacheComposition(AnimationViewHandler viewHandler, IAnimationView animView)
        {
            viewHandler.NativeView.SetCacheComposition(animView.CacheComposition);
        }
        private static void MapFallbackResource(AnimationViewHandler viewHandler, IAnimationView animView)
        {
            // Empty
        }
        private static void MapMinFrame(AnimationViewHandler viewHandler, IAnimationView animView)
        {
            viewHandler.NativeView.SetMinAndMaxFrame(animView.MinFrame, animView.MaxFrame);
        }
        private static void MapMinProgress(AnimationViewHandler viewHandler, IAnimationView animView)
        {
            viewHandler.NativeView.SetMinProgress(animView.MinProgress);
        }
        private static void MapMaxFrame(AnimationViewHandler viewHandler, IAnimationView animView)
        {
            viewHandler.NativeView.SetMinAndMaxFrame(animView.MinFrame, animView.MaxFrame);
        }
        private static void MapMaxProgress(AnimationViewHandler viewHandler, IAnimationView animView)
        {
            viewHandler.NativeView.SetMaxProgress(animView.MaxProgress);
        }
        private static void MapSpeed(AnimationViewHandler viewHandler, IAnimationView animView)
        {
            viewHandler.NativeView.Speed = animView.Speed;
        }
        private static void MapRepeatMode(AnimationViewHandler viewHandler, IAnimationView animView)
        {
            viewHandler.NativeView.ConfigureRepeat(animView.RepeatMode, animView.RepeatCount);
        }
        private static void MapRepeatCount(AnimationViewHandler viewHandler, IAnimationView animView)
        {
            MapRepeatMode(viewHandler, animView);
        }
        private static void MapIsAnimating(AnimationViewHandler viewHandler, IAnimationView animView)
        {
            // Empty
        }
        private static void MapImageAssetsFolder(AnimationViewHandler viewHandler, IAnimationView animView)
        {
            if (!string.IsNullOrEmpty(animView.ImageAssetsFolder))
            {
                viewHandler.NativeView.ImageAssetsFolder = animView.ImageAssetsFolder;
            }
        }
        private static void MapProgress(AnimationViewHandler viewHandler, IAnimationView animView)
        {
            viewHandler.NativeView.Progress = animView.Progress;
        }
        private static void MapDuration(AnimationViewHandler viewHandler, IAnimationView animView)
        {
            // Empty
        }
        private static void MapAutoPlay(AnimationViewHandler viewHandler, IAnimationView animView)
        {
            // Empty
        }
        private static void MapCommand(AnimationViewHandler viewHandler, IAnimationView animView)
        {
            // Empty
        }
        private static void MapEnableMergePathsForKitKatAndAbove(AnimationViewHandler viewHandler, IAnimationView animView)
        {
            viewHandler.NativeView.EnableMergePathsForKitKatAndAbove(animView.EnableMergePathsForKitKatAndAbove);
        }

        #endregion


        protected override LottieAnimationView CreatePlatformView() => new LottieAnimationView(Context);

        protected override void ConnectHandler(LottieAnimationView platformView)
        {
            NativeView = platformView;

            _animatorListener = new AnimatorListener
            {
                OnAnimationCancelImpl = () => VirtualView.InvokeStopAnimation(),
                OnAnimationEndImpl = () => VirtualView.InvokeFinishedAnimation(),
                OnAnimationPauseImpl = () => VirtualView.InvokePauseAnimation(),
                OnAnimationRepeatImpl = () => VirtualView.InvokeRepeatAnimation(),
                OnAnimationResumeImpl = () => VirtualView.InvokeResumeAnimation(),
                OnAnimationStartImpl = () => VirtualView.InvokePlayAnimation()
            };
            _animatorUpdateListener = new AnimatorUpdateListener
            {
                OnAnimationUpdateImpl = (progress) => VirtualView.InvokeAnimationUpdate(progress)
            };
            _lottieOnCompositionLoadedListener = new LottieOnCompositionLoadedListener
            {
                OnCompositionLoadedImpl = (composition) => VirtualView.InvokeAnimationLoaded(composition)
            };
            _lottieFailureListener = new LottieFailureListener
            {
                OnResultImpl = (exception) => VirtualView.InvokeFailure(exception)
            };
            _clickListener = new ClickListener
            {
                OnClickImpl = () => VirtualView.InvokeClick()
            };

            NativeView.AddAnimatorListener(_animatorListener);
            NativeView.AddAnimatorUpdateListener(_animatorUpdateListener);
            NativeView.AddLottieOnCompositionLoadedListener(_lottieOnCompositionLoadedListener);
            NativeView.SetFailureListener(_lottieFailureListener);
            NativeView.SetOnClickListener(_clickListener);

            VirtualView.PlayCommand = new Command(() => NativeView.PlayAnimation());
            VirtualView.PauseCommand = new Command(() => NativeView.PauseAnimation());
            VirtualView.ResumeCommand = new Command(() => NativeView.ResumeAnimation());
            VirtualView.StopCommand = new Command(() =>
            {
                NativeView.CancelAnimation();
                NativeView.Progress = 0.0f;
            });
            VirtualView.ClickCommand = new Command(() => NativeView.PerformClick());

            VirtualView.PlayMinAndMaxFrameCommand = new Command((object paramter) =>
            {
                if (paramter is (int minFrame, int maxFrame))
                {
                    NativeView.SetMinAndMaxFrame(minFrame, maxFrame);
                    NativeView.PlayAnimation();
                }
            });
            VirtualView.PlayMinAndMaxProgressCommand = new Command((object paramter) =>
            {
                if (paramter is (float minProgress, float maxProgress))
                {
                    NativeView.SetMinAndMaxProgress(minProgress, maxProgress);
                    NativeView.PlayAnimation();
                }
            });
            VirtualView.ReverseAnimationSpeedCommand = new Command(() => NativeView.ReverseAnimationSpeed());

            NativeView.SetCacheComposition(VirtualView.CacheComposition);


            //if (VirtualView.MinFrame != int.MinValue)
            //    NativeView.SetMinFrame(VirtualView.MinFrame);
            //if (VirtualView.MinProgress != float.MinValue)
            //    NativeView.SetMinProgress(VirtualView.MinProgress);
            //if (VirtualView.MaxFrame != int.MinValue)
            //    NativeView.SetMaxFrame(VirtualView.MaxFrame);
            //if (VirtualView.MaxProgress != float.MinValue)
            //    NativeView.SetMaxProgress(VirtualView.MaxProgress);

            NativeView.Speed = VirtualView.Speed;

            NativeView.ConfigureRepeat(VirtualView.RepeatMode, VirtualView.RepeatCount);

            if (!string.IsNullOrEmpty(VirtualView.ImageAssetsFolder))
                NativeView.ImageAssetsFolder = VirtualView.ImageAssetsFolder;

            NativeView.Progress = VirtualView.Progress;

            NativeView.EnableMergePathsForKitKatAndAbove(VirtualView.EnableMergePathsForKitKatAndAbove);

            VirtualView.Duration = NativeView.Duration;
            VirtualView.IsAnimating = NativeView.IsAnimating;

            base.ConnectHandler(platformView);
        }

        protected override void DisconnectHandler(LottieAnimationView platformView)
        {
            NativeView.RemoveAnimatorListener(_animatorListener);
            NativeView.RemoveAllUpdateListeners();
            NativeView.RemoveLottieOnCompositionLoadedListener(_lottieOnCompositionLoadedListener);
            NativeView.SetFailureListener(null);
            NativeView.SetOnClickListener(null);

            base.DisconnectHandler(platformView);
        }
    }
}
#endif


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lottie.MAUI
{
    public interface IAnimationView : IView
    {
        object Animation { get; set; }

        AnimationSource AnimationSource { get; set; } // = AnimationSource.AssetOrBundle;

        bool CacheComposition { get; set; }

        ImageSource FallbackResource { get; set; }

        int MinFrame { get; set; }

        float MinProgress { get; set; }

        int MaxFrame { get; set; }

        float MaxProgress { get; set; }

        float Speed { get; set; }

        RepeatMode RepeatMode { get; set; }

        int RepeatCount { get; set; }

        bool IsAnimating { get; set; }

        string ImageAssetsFolder { get; set; }

        float Progress { get; set; }

        long Duration { get; set; }

        bool AutoPlay { get; set; }

        ICommand Command { get; set; }

        bool EnableMergePathsForKitKatAndAbove { get; set; }



        internal void InvokePlayAnimation();

        internal void InvokeResumeAnimation();

        internal void InvokeStopAnimation();

        internal void InvokePauseAnimation();

        internal void InvokeRepeatAnimation();

        internal void InvokeAnimationUpdate(float progress);

        internal void InvokeAnimationLoaded(object animation);

        internal void InvokeFailure(Exception ex);

        internal void InvokeFinishedAnimation();
        internal void InvokeClick();


        ICommand PlayCommand { get; set; }
        ICommand PauseCommand { get; set; }
        ICommand ResumeCommand { get; set; }
        ICommand StopCommand { get; set; }
        ICommand ClickCommand { get; set; }
        ICommand PlayMinAndMaxFrameCommand { get; set; }
        ICommand PlayMinAndMaxProgressCommand { get; set; }
        ICommand ReverseAnimationSpeedCommand { get; set; }
    }
}

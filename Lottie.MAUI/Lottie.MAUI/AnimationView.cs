using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lottie.MAUI
{
    public class AnimationView : View, IAnimationView
    {
        public AnimationView() 
            : base()
        {

        }

        #region IAnimationView

        public object Animation { get; set; } = default(object);

        public AnimationSource AnimationSource { get; set; } = AnimationSource.AssetOrBundle;

        public bool CacheComposition { get; set; } = true;

        public ImageSource FallbackResource { get; set; } = default(ImageSource);

        public int MinFrame { get; set; } = int.MinValue;

        public float MinProgress { get; set; } = 0; /*float.MinValue;*/

        public int MaxFrame { get; set; } = 0;//int.MinValue;

        public float MaxProgress { get; set; } = 0;//float.MinValue;

        public float Speed { get; set; } = 1.0f;

        public RepeatMode RepeatMode { get; set; } = Lottie.MAUI.RepeatMode.Restart;

        public int RepeatCount { get; set; } = 0;

        public bool IsAnimating { get; set; } = false;

        public string ImageAssetsFolder { get; set; } = default(string);

        public float Progress { get; set; } = 0.0f;

        public long Duration { get; set; } = default(long);

        public bool AutoPlay { get; set; } = true;

        public ICommand Command { get; set; }

        public bool EnableMergePathsForKitKatAndAbove { get; set; } = false;

        #endregion IAnimationView


        /// <summary>
        /// Called when the Lottie animation starts playing
        /// </summary>
        public event EventHandler OnPlayAnimation;

        /// <summary>
        /// Called when the Lottie animation is paused
        /// </summary>
        public event EventHandler OnPauseAnimation;

        /// <summary>
        /// Called when the Lottie animation is resumed after pausing
        /// </summary>
        public event EventHandler OnResumeAnimation;

        /// <summary>
        /// Called when the Lottie animation is stopped
        /// </summary>
        public event EventHandler OnStopAnimation;

        /// <summary>
        /// Called when the Lottie animation is repeated
        /// </summary>
        public event EventHandler OnRepeatAnimation;

        /// <summary>
        /// Called when the Lottie animation is clicked
        /// </summary>
        public event EventHandler Clicked;

        /// <summary>
        /// Called when the Lottie animation is playing with the current progress
        /// </summary>
        public event EventHandler<float> OnAnimationUpdate;

        /// <summary>
        /// Called when the Lottie animation is loaded with the Lottie Composition as parameter
        /// </summary>
        public event EventHandler<object> OnAnimationLoaded;

        /// <summary>
        /// Called when the animation fails to load or when an exception happened when trying to play
        /// </summary>
        public event EventHandler<Exception> OnFailure;

        /// <summary>
        /// Called when the Lottie animation is finished playing
        /// </summary>
        public event EventHandler OnFinishedAnimation;

        public void InvokePlayAnimation()
        {
            OnPlayAnimation?.Invoke(this, EventArgs.Empty);
        }

        public void InvokeResumeAnimation()
        {
            OnResumeAnimation?.Invoke(this, EventArgs.Empty);
        }

        public void InvokeStopAnimation()
        {
            OnStopAnimation?.Invoke(this, EventArgs.Empty);
        }

        public void InvokePauseAnimation()
        {
            OnPauseAnimation?.Invoke(this, EventArgs.Empty);
        }

        public void InvokeRepeatAnimation()
        {
            OnRepeatAnimation?.Invoke(this, EventArgs.Empty);
        }

        public void InvokeAnimationUpdate(float progress)
        {
            OnAnimationUpdate?.Invoke(this, progress);
        }

        public void InvokeAnimationLoaded(object animation)
        {
            OnAnimationLoaded?.Invoke(this, animation);
        }

        public void InvokeFailure(Exception ex)
        {
            OnFailure?.Invoke(this, ex);
        }

        public void InvokeFinishedAnimation()
        {
            OnFinishedAnimation?.Invoke(this, EventArgs.Empty);
        }

        public void InvokeClick()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
            Command?.ExecuteCommandIfPossible(this);
        }

        public ICommand PlayCommand { get; set; }
        public ICommand PauseCommand { get; set; }
        public ICommand ResumeCommand { get; set; }
        public ICommand StopCommand { get; set; }
        public ICommand ClickCommand { get; set; }
        public ICommand PlayMinAndMaxFrameCommand { get; set; }
        public ICommand PlayMinAndMaxProgressCommand { get; set; }
        public ICommand ReverseAnimationSpeedCommand { get; set; }

        /// <summary>
        /// Simulate a click action on the view
        /// </summary>
        public void Click()
        {
            ClickCommand.ExecuteCommandIfPossible(this);
        }

        /// <summary>
        /// Plays the animation from the beginning. If speed is < 0, it will start at the end and play towards the beginning
        /// </summary>
        public void PlayAnimation()
        {
            PlayCommand.ExecuteCommandIfPossible();
        }

        /// <summary>
        /// Continues playing the animation from its current position. If speed < 0, it will play backwards from the current position.
        /// </summary>
        public void ResumeAnimation()
        {
            ResumeCommand.ExecuteCommandIfPossible();
        }

        /// <summary>
        /// Will stop and reset the currently playing animation
        /// </summary>
        public void StopAnimation()
        {
            StopCommand.ExecuteCommandIfPossible();
        }

        /// <summary>
        /// Will pause the currently playing animation. Call ResumeAnimation to continue
        /// </summary>
        public void PauseAnimation()
        {
            PauseCommand.ExecuteCommandIfPossible();
        }

        public void PlayMinAndMaxFrame(int minFrame, int maxFrame)
        {
            PlayMinAndMaxFrameCommand.ExecuteCommandIfPossible((minFrame, maxFrame));
        }

        public void PlayMinAndMaxProgress(float minProgress, float maxProgress)
        {
            PlayMinAndMaxProgressCommand.ExecuteCommandIfPossible((minProgress, maxProgress));
        }

        /// <summary>
        /// Reverses the current animation speed. This does NOT play the animation.
        /// </summary>
        public void ReverseAnimationSpeed()
        {
            ReverseAnimationSpeedCommand.ExecuteCommandIfPossible();
        }

        public void SetAnimationFromAssetOrBundle(string path)
        {
            AnimationSource = AnimationSource.AssetOrBundle;
            Animation = path;
        }

        public void SetAnimationFromEmbeddedResource(string resourceName, Assembly assembly = null)
        {
            AnimationSource = AnimationSource.EmbeddedResource;

#if ANDROID

            if (assembly == null)
                assembly = Microsoft.Maui.MauiApplication.Current.GetType().Assembly;
#endif

            Animation = $"resource://{resourceName}?assembly={Uri.EscapeUriString(assembly.FullName)}";
        }

        public void SetAnimationFromJson(string json)
        {
            AnimationSource = AnimationSource.Json;
            Animation = json;
        }

        public void SetAnimationFromUrl(string url)
        {
            AnimationSource = AnimationSource.Url;
            Animation = url;
        }

        public void SetAnimationFromStream(Stream stream)
        {
            AnimationSource = AnimationSource.Stream;
            Animation = stream;
        }
    }
}

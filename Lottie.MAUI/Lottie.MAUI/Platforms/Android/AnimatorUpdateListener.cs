using Android.Animation;
using Android.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottie.MAUI.Platforms.Android
{
    internal class AnimatorUpdateListener : Java.Lang.Object, ValueAnimator.IAnimatorUpdateListener
    {
        public AnimatorUpdateListener()
        {
        }

        public AnimatorUpdateListener(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {
        }

        public Action<float> OnAnimationUpdateImpl { get; set; }

        public void OnAnimationUpdate(ValueAnimator animation)
        {
            if (animation == null)
                throw new ArgumentNullException(nameof(animation));

            OnAnimationUpdateImpl?.Invoke(((float)animation.AnimatedValue));
        }
    }
}

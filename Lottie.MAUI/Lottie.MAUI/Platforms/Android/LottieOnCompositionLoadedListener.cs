using Android.Runtime;
using Com.Airbnb.Lottie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottie.MAUI.Platforms.Android
{
    internal class LottieOnCompositionLoadedListener : Java.Lang.Object, ILottieOnCompositionLoadedListener
    {
        public LottieOnCompositionLoadedListener()
        {
        }

        public LottieOnCompositionLoadedListener(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {
        }

        public Action<object> OnCompositionLoadedImpl { get; set; }

        public void OnCompositionLoaded(LottieComposition p0)
        {
            OnCompositionLoadedImpl?.Invoke(p0);
        }
    }
}

#if ANDROID
using Lottie.MAUI.Platforms.Android;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottie.MAUI.Extensions
{
    public static class MauiAppBuilderExtension
    {
        public static MauiAppBuilder UseLottieAnimations(this MauiAppBuilder builder)
        {
            return builder.ConfigureMauiHandlers(collection =>
            {
#if ANDROID
                collection.AddHandler(typeof(AnimationView), typeof(AnimationViewHandler));
#endif
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lottie.MAUI
{
#if ANDROID
    public static class AnimationViewExtensions
    {
        public static Stream GetStreamFromAssembly(this AnimationView animationView)
        {
            if (animationView == null)
                throw new ArgumentNullException(nameof(animationView));

            if (animationView.Animation is string embeddedAnimation)
            {
                Assembly assembly = null;
                string resourceName = null;

                if (embeddedAnimation.StartsWith("resource://", StringComparison.OrdinalIgnoreCase))
                {
                    var uri = new Uri(embeddedAnimation);

                    var parts = uri.OriginalString.Substring(11).Split('?');
                    resourceName = parts.First();

                    if (parts.Length > 1)
                    {
                        var name = Uri.UnescapeDataString(uri.Query.Substring(10));
                        var assemblyName = new AssemblyName(name);
                        assembly = Assembly.Load(assemblyName);
                    }

                    if (assembly == null)
                    {
                        var callingAssemblyMethod = typeof(Assembly).GetTypeInfo().GetDeclaredMethod("GetCallingAssembly");
                        assembly = (Assembly)callingAssemblyMethod.Invoke(null, Array.Empty<object>());
                    }
                }

                if (assembly == null)
                    assembly = Microsoft.Maui.MauiApplication.Current.GetType().Assembly;

                if (string.IsNullOrEmpty(resourceName))
                    resourceName = embeddedAnimation;

                var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{resourceName}");

                if (stream == null)
                {
                    return null;
                    //throw new FileNotFoundException("Cannot find file.", embeddedAnimation);
                }
                return stream;
            }
            return null;
        }
    }
#endif
}

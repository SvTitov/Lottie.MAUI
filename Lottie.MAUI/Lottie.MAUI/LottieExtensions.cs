﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lottie.MAUI
{
    public static class LottieExtensions
    {
        public static void ExecuteCommandIfPossible(this ICommand command, object parameter = null)
        {
            if (command?.CanExecute(parameter) == true)
            {
                command.Execute(parameter);
            }
        }
    }
}

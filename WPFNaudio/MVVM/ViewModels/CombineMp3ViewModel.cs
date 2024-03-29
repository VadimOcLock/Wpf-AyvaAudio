﻿using System.Windows;
using WPFNaudio.Core;
using WPFNaudio.MVVM.ViewModels.Base;

namespace WPFNaudio.MVVM.ViewModels
{
    public class CombineMp3ViewModel : ViewModel
    {
        public LambdaCommand CloseWindowCommand { get; }

        public CombineMp3ViewModel()
        {
            CloseWindowCommand = new LambdaCommand(OnCloseWindowCommandExecuted, CanCloseWindowCommandExecute);
        }

        private bool CanCloseWindowCommandExecute(object p) => true;
        private void OnCloseWindowCommandExecuted(object p)
        {
            Window? w = p as Window;
            if (w != null)
                w.Close();
        }
    }
}

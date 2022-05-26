using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFNaudio.Core;
using WPFNaudio.MVVM.ViewModels.Base;

namespace WPFNaudio.MVVM.ViewModels
{
    public class VolumeEditViewModel : ViewModel
    {
        public LambdaCommand CloseWindowCommand { get; }

        private bool CanCloseWindowCommandExecute(object p) => true;
        private void OnCloseWindowCommandExecuted(object p)
        {
            Window? w = p as Window;
            if (w != null)
                w.Close();
        }

        public VolumeEditViewModel()
        {
            CloseWindowCommand = new LambdaCommand(OnCloseWindowCommandExecuted, CanCloseWindowCommandExecute);
        }
    }
}

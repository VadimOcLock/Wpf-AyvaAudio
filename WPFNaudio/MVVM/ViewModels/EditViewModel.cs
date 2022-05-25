using System;
using WPFNaudio.MVVM.ViewModels.Base;

namespace WPFNaudio.MVVM.ViewModels
{
    public class EditViewModel : ViewModel
    {
        private double _volumeValue = 1;

        public double VolumeValue
        {
            get
            {
                return Math.Round(_volumeValue,1);
            }
            set
            {
                Set(ref _volumeValue, value);
            }
        }


        public EditViewModel()
        {

        }
    }
}

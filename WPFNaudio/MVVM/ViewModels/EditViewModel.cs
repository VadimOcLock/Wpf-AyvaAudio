using System;
using System.ComponentModel;
using WPFNaudio.MVVM.ViewModels.Base;

namespace WPFNaudio.MVVM.ViewModels
{
    public class EditViewModel : ViewModel, IDataErrorInfo
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
                if (value < 0)
                {
                    Set(ref _volumeValue, 0);
                }
                else if (value > 200)
                {
                    Set(ref _volumeValue, 200);
                }
                else
                {
                    Set(ref _volumeValue, value);
                }
            }
        }

        public string Error { get { return null; } }

        public string this[string name] 
        {
            get 
            {
                string result = null;

                switch (name)
                {
                    case "VolumeValue":
                        if (VolumeValue == null)
                            result = "Поле не может быть пустым";
                        else if (VolumeValue < 0 || VolumeValue > 200)
                            result = "Введите значение от 0 до 200";
                        break;
                }

                return result;
            }
        }

        public EditViewModel()
        {

        }
    }
}

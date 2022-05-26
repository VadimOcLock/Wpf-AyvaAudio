using WPFNaudio.Core;
using WPFNaudio.MVVM.View.Windows;
using WPFNaudio.MVVM.ViewModels.Base;

namespace WPFNaudio.MVVM.ViewModels
{
    public class ConvertViewModel : ViewModel
    {
        private LambdaCommand _openWavConvertWindowCommand;

        public LambdaCommand OpenWavConvertWindowCommand
        {
            get 
            {
                return _openWavConvertWindowCommand ??= new LambdaCommand(obj =>
                    {
                        var window = new WavConvertView();
                        var vm = new WavConvertViewModel();
                        vm.OpenFile();

                        window.DataContext = vm;
                        if (window.ShowDialog() == true)
                        {
                        }
                    });
            }
        }


        public ConvertViewModel()
        {

        }
    }
}

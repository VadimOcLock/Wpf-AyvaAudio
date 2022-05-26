using WPFNaudio.Core;
using WPFNaudio.MVVM.View.Windows;
using WPFNaudio.MVVM.ViewModels.Base;

namespace WPFNaudio.MVVM.ViewModels
{
    public class ConvertViewModel : ViewModel
    {
        public LambdaCommand OpenWavConvertWindowCommand { get; }

        private bool CanOpenWavConvertWindowCommandExecute(object p) => true;
        private void OnOpenWavConvertWindowCommandExecuted(object p)
        {
            var window = new WavConvertView();
            var vm = new WavConvertViewModel();

            vm.OpenFileDialog();

            window.DataContext = vm;
            if (window.ShowDialog() == true) { }
        }

        public ConvertViewModel()
        {
            OpenWavConvertWindowCommand = new LambdaCommand(OnOpenWavConvertWindowCommandExecuted, CanOpenWavConvertWindowCommandExecute);
        }
    }
}

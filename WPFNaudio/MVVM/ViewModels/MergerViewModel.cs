using WPFNaudio.Core;
using WPFNaudio.MVVM.View.Windows;
using WPFNaudio.MVVM.ViewModels.Base;

namespace WPFNaudio.MVVM.ViewModels
{
    public class MergerViewModel : ViewModel
    {
        public LambdaCommand MergeMp3Command { get; }
        public LambdaCommand CombineMp3Command { get; }

        public MergerViewModel()
        {
            MergeMp3Command = new LambdaCommand(OnMergeMp3CommandExecuted, CanMergeMp3CommandExecute);
            CombineMp3Command = new LambdaCommand(OnCombineMp3CommandExecuted, CanCombineMp3CommandExecute);
        }

        private bool CanMergeMp3CommandExecute(object p) => true;
        private void OnMergeMp3CommandExecuted(object p)
        {
            var window = new MergeMp3View();
            var vm = new MergeMp3ViewModel();

            window.DataContext = vm;
            if (window.ShowDialog() == true) { }
        }
        private bool CanCombineMp3CommandExecute(object p) => true;
        private void OnCombineMp3CommandExecuted(object p)
        {
            var window = new CombineMp3View();
            var vm = new CombineMp3ViewModel();

            window.DataContext = vm;
            if (window.ShowDialog() == true) { }
        }
    }
}

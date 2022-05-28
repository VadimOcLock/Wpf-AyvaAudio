using System.Windows;
using System.Windows.Input;

namespace WPFNaudio.MVVM.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для CutWavFileView.xaml
    /// </summary>
    public partial class CutWavFileView : Window
    {
        public CutWavFileView()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}

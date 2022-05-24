using System.Windows;
using System.Windows.Input;

namespace WPFNaudio.MVVM.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для WavConvertView.xaml
    /// </summary>
    public partial class WavConvertView : Window
    {
        public WavConvertView()
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

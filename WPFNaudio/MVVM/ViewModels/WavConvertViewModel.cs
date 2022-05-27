using Microsoft.Win32;
using NAudio.Wave;
using System.IO;
using System.Windows;
using WPFNaudio.Core;
using WPFNaudio.MVVM.ViewModels.Base;

namespace WPFNaudio.MVVM.ViewModels
{
    public class WavConvertViewModel : ViewModel
    {
        private string _wavConvertResult = "Ошибка";
        public string WavConvertResult { get => _wavConvertResult; }

        private string _inputFile = "ERROR_PATH";
        public string InputFile { get => _inputFile; }

        private string _outputFile = "ERROR_PATH";
        public string OutputFile { get => _outputFile; }

        public LambdaCommand CloseWindowCommand { get; }

        private bool CanCloseWindowCommandExecute(object p) => true;
        private void OnCloseWindowCommandExecuted(object p)
        {
            Window? w = p as Window;
            if (w != null)
                w.Close();
        }

        public void OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 File (*.mp3)|*.mp3";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == true)
            {
                _inputFile = openFileDialog.FileName;
                _outputFile = Path.GetDirectoryName(_inputFile) + @"\" + Path.GetFileNameWithoutExtension(_inputFile) + "_converted.wav";

                using (var fileReader = new Mp3FileReader(_inputFile))
                {
                    WaveFileWriter.CreateWaveFile(_outputFile, fileReader);
                }

                _wavConvertResult = "Успешно";
            }
        }
        public WavConvertViewModel()
        {
            CloseWindowCommand = new LambdaCommand(OnCloseWindowCommandExecuted, CanCloseWindowCommandExecute);
        }
    }
}

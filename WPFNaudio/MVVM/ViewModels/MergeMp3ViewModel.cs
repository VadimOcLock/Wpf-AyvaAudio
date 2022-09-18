using Microsoft.Win32;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using WPFNaudio.Core;
using WPFNaudio.MVVM.ViewModels.Base;

namespace WPFNaudio.MVVM.ViewModels
{
    public class MergeMp3ViewModel : ViewModel
    {
        public LambdaCommand CloseWindowCommand { get; }

        private StringBuilder _inputFiles;
        public StringBuilder InputFiles { get => _inputFiles; }
        private string _outputFile;
        public string OutputFile { get => _outputFile; }

        public MergeMp3ViewModel()
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
        public void MergeMp3()
        {
            //OpenFileDialog openFileDialog = new OpenFileDialog();

            //openFileDialog.Filter = "Wave File (*.mp3)|*.mp3";
            //openFileDialog.Multiselect = true;
            //if (openFileDialog.ShowDialog() == true)
            //{
            //    InputFiles.Append(openFileDialog.FileNames);
            //    _outputFile = Path.GetDirectoryName(InputFiles.ToString()) + @"\" + Path.GetFileNameWithoutExtension(InputFiles.ToString()) + "_mixed.mp3";
            //    List<AudioFileReader> files = new List<AudioFileReader>();
            //    foreach (string item in openFileDialog.FileNames)
            //    {
            //        files.Add(new AudioFileReader(item));
            //    }
            //    var mix = new MixingSampleProvider(files);
            //    WaveFileWriter.CreateWaveFile16(OutputFile, mix);
            //}
        }
    }
}

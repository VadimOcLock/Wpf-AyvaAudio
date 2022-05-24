﻿using Microsoft.Win32;
using NAudio.Wave;
using System.IO;
using System.Windows;
using WPFNaudio.Core;
using WPFNaudio.MVVM.ViewModels.Base;

namespace WPFNaudio.MVVM.ViewModels
{
    public class WavConvertViewModel : ViewModel
    {
        private string _inputFile = "ERROR_PATH";
        public string InputFile
        {
            get { return _inputFile; }
            set 
            { 
                Set(ref _inputFile, value);
            }
        }

        private string _outputFile = "ERROR_PATH";
        public string OutputFile
        {
            get { return _outputFile; }
            set
            {
                Set(ref _outputFile, value);
            }
        }



        private LambdaCommand _closeWindowCommand;

        public LambdaCommand CloseWindowCommand
        {
            get
            {
                return _closeWindowCommand ??
                    (_closeWindowCommand = new LambdaCommand(obj =>
                    {
                        Window w = obj as Window;
                        if (w != null)
                        {
                            w.Close();
                        }
                    }));
            }
        }

        private void OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 File (*.mp3)|*.mp3";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == true)
            {
                InputFile = openFileDialog.FileName;
                OutputFile = Path.GetDirectoryName(_inputFile) + @"\" + Path.GetFileNameWithoutExtension(_inputFile) + "_converted.wav";

                using (var fileReader = new Mp3FileReader(_inputFile))
                {
                    WaveFileWriter.CreateWaveFile(_outputFile, fileReader);
                }
            }
        }

        public void OpenFile()
        {
            OpenFileDialog();
        }

        public WavConvertViewModel()
        {

        }
    }
}

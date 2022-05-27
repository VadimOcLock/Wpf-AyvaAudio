using Microsoft.Win32;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFNaudio.Core;
using WPFNaudio.MVVM.ViewModels.Base;

namespace WPFNaudio.MVVM.ViewModels
{
    public class VolumeEditViewModel : ViewModel
    {
        public LambdaCommand CloseWindowCommand { get; }

        private string _inputFile = "ERROR_PATH";
        public string InputFile
        {
            get => _inputFile;
            set => Set(ref _inputFile, value);
        }

        private string _outputFile = "ERROR_PATH";
        public string OutputFile
        {
            get => _outputFile;
            set => Set(ref _outputFile, value);
        }

        private double _volumeFactor = 0;
        public double VolumeFactor
        {
            get => _volumeFactor;
            set => Set(ref _volumeFactor, value);
        }

        public VolumeEditViewModel()
        {
            CloseWindowCommand = new LambdaCommand(OnCloseWindowCommandExecuted, CanCloseWindowCommandExecute);
        }

        public void VolumeEditFile(double volumeFactor)
        {
            VolumeFactor = volumeFactor;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Wave File (*.wav)|*.wav";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == true)
            {
                InputFile = openFileDialog.FileName;
                OutputFile = Path.GetDirectoryName(_inputFile) + @"\" + Path.GetFileNameWithoutExtension(_inputFile) + "_volumeChanged.wav";

                using (WaveFileReader waveFileReader = new WaveFileReader(_inputFile))
                {
                    WaveFormat waveFormat = waveFileReader.WaveFormat;

                    int sampleCount = (int)waveFileReader.SampleCount;
                    byte[] buffer = new byte[waveFileReader.SampleCount * 2];
                    waveFileReader.Read(buffer, 0, sampleCount);

                    short[] shortBuffer = new short[sampleCount];
                    for (int i = 0; i < shortBuffer.Length; i++)
                    {
                        shortBuffer[i] = BitConverter.ToInt16(buffer, i * 2);
                    }

                    for (int i = 0; i < shortBuffer.Length; i++)
                    {
                        shortBuffer[i] = (short)(shortBuffer[i] * volumeFactor / 1000);
                    }

                    WaveFileWriter waveFileWriter = new WaveFileWriter(_outputFile, waveFormat);
                    waveFileWriter.WriteSamples(shortBuffer, 0, shortBuffer.Length);
                    waveFileWriter.Close();
                }
            }
        }
        private bool CanCloseWindowCommandExecute(object p) => true;
        private void OnCloseWindowCommandExecuted(object p)
        {
            Window? w = p as Window;
            if (w != null)
                w.Close();
        }
    }
}

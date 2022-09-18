using Microsoft.Win32;
using NAudio.Wave;
using System;
using System.IO;
using System.Windows;
using WPFNaudio.Core;
using WPFNaudio.MVVM.ViewModels.Base;

namespace WPFNaudio.MVVM.ViewModels
{
    internal class CutWavFileViewModel : ViewModel
    {
        public LambdaCommand CloseWindowCommand { get; }

        private string _resultCut = "Ошибка";
        public string ResultCut { get => _resultCut; }

        private string _inputFile = "ERROR_PATH";
        public string InputFile { get => _inputFile; }

        private string _outputFile = "ERROR_PATH";
        public string OutputFile { get => _outputFile; }

        private int _cutStartTiming = 0;
        public int CutStartTiming { get => _cutStartTiming; }

        private int _cutEndTiming = 0;
        public int CutEndTiming { get => _cutEndTiming; }

        public CutWavFileViewModel()
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

        public void CutWavFile(int cutStartTiming , int cutEndTiming)
        {
            _cutStartTiming = cutStartTiming;
            _cutEndTiming = cutEndTiming;

            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Wave File (*.wav)|*.wav";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == true)
            {
                _inputFile = openFileDialog.FileName;
                _outputFile = Path.GetDirectoryName(InputFile) + @"\" + Path.GetFileNameWithoutExtension(InputFile) + "_cut.wav";
                using (WaveFileReader waveFileReader = new WaveFileReader(InputFile))
                {
                    //waveFileReader.TotalTime.TotalSeconds
                    TrimWav(new TimeSpan(0, 0, cutStartTiming), new TimeSpan(0, 0, cutEndTiming), InputFile, OutputFile);
                }

                _resultCut = "Успешно";
            }
        }
        private void TrimWav(TimeSpan cutStart, TimeSpan cutEnd, string inputPath, string outputPath)
        {
            using (WaveFileReader fileReader = new WaveFileReader(inputPath))
            {
                cutEnd = new TimeSpan(0, 0, (int)(fileReader.TotalTime.TotalSeconds - cutEnd.TotalSeconds));
                using (WaveFileWriter fileWriter = new WaveFileWriter(outputPath, fileReader.WaveFormat))
                {
                    int bytPerMillisec = fileReader.WaveFormat.AverageBytesPerSecond / 1000;

                    int startPos = (int)cutStart.TotalMilliseconds * bytPerMillisec;
                    startPos = startPos - startPos % fileReader.WaveFormat.BlockAlign;

                    int endByt = (int)cutEnd.TotalMilliseconds * bytPerMillisec;
                    endByt = endByt - endByt % fileReader.WaveFormat.BlockAlign;
                    int endPos = (int)fileReader.Length - endByt;

                    Trimming(fileReader, fileWriter, startPos, endPos);
                }
            }
        }
        private void Trimming(WaveFileReader fileReader, WaveFileWriter fileWriter, int start, int end)
        {
            fileReader.Position = start;
            byte[] buff = new byte[1024];
            while (fileReader.Position < end)
            {
                int bytesRequired = (int)(end - fileReader.Position);
                if (bytesRequired > 0)
                {
                    int bytesRead = Math.Min(bytesRequired, buff.Length);
                    bytesRead = fileReader.Read(buff, 0, bytesRead);
                    if (bytesRead > 0)
                        fileWriter.WriteData(buff, 0, bytesRead);
                }
            }
        }
    }
}

using System;
using System.ComponentModel;
using WPFNaudio.Core;
using WPFNaudio.MVVM.View.Windows;
using WPFNaudio.MVVM.ViewModels.Base;

namespace WPFNaudio.MVVM.ViewModels
{
    public class EditViewModel : ViewModel
    {
        private double _volumeValue = 1;
        public double VolumeValue
        {
            get => Math.Round(_volumeValue, 1);
            set
            {
                if (value < 0)
                    Set(ref _volumeValue, 0);
                else if (value > 200)
                    Set(ref _volumeValue, 200);
                else
                    Set(ref _volumeValue, value);
            }
        }

        private int _cutStartTiming = 0;

        public int CutStartTiming
        {
            get => _cutStartTiming; 
            set 
            {
                if (value < 0)
                    Set(ref _cutStartTiming, 0);
                else
                    Set(ref _cutStartTiming, value);
            }
        }

        private int _cutEndTiming = 0;

        public int CutEndTiming
        {
            get => _cutEndTiming;
            set
            {
                if (value < CutStartTiming)
                    Set(ref _cutEndTiming, CutStartTiming);
                else
                    Set(ref _cutEndTiming, value);
            }
        }

        public LambdaCommand VolumeEditCommand { get; }
        public LambdaCommand CutWavFileCommand { get; }

        private bool CanVolumeEditCommandExecute(object p) => true;
        private void OnVolumeEditCommandExecuted(object p)
        {
            var window = new VolumeEditView();
            var vm = new VolumeEditViewModel();

            vm.VolumeEditFile(VolumeValue);

            window.DataContext = vm;
            if (window.ShowDialog() == true) { }
        }

        private bool CanCutWavFileCommandExecute(object p) => true;
        private void OnCutWavFileCommandExecuted(object p)
        {
            var window = new CutWavFileView();
            var vm = new CutWavFileViewModel();

            vm.CutWavFile(CutStartTiming, CutEndTiming);

            window.DataContext = vm;
            if (window.ShowDialog() == true) { }
        }

        public EditViewModel()
        {
            VolumeEditCommand = new LambdaCommand(OnVolumeEditCommandExecuted, CanVolumeEditCommandExecute);
            CutWavFileCommand = new LambdaCommand(OnCutWavFileCommandExecuted, CanCutWavFileCommandExecute);
        }
    }
}

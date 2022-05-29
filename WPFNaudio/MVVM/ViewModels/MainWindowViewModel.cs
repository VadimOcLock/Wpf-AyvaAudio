using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Windows;
using WPFNaudio.Core;
using WPFNaudio.MVVM.ViewModels.Base;

namespace WPFNaudio.MVVM.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private const string GITHUB_URL = "https://github.com/VadimOcLock/Wpf-AyvaAudio";
        
        public LambdaCommand HomeViewCommand { get; }
        public LambdaCommand ConvertViewCommand { get; }
        public LambdaCommand EditViewCommand { get; }
        public LambdaCommand MergerViewCommand { get; }
        public LambdaCommand GitHubLinkCommand { get; }

        public LambdaCommand CloseWindowCommand { get; }
        public LambdaCommand ShareWindowCommand { get; }
        public LambdaCommand TurnWindowCommand { get; }

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set => Set(ref _currentView, value);
        }

        public MainWindowViewModel()
        {
            var HomeVM = new HomeViewModel();
            var ConvertVM = new ConvertViewModel();
            var EditVM = new EditViewModel();
            var MergerVM = new MergerViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new LambdaCommand(o =>
            {
                CurrentView = HomeVM;
            });

            ConvertViewCommand = new LambdaCommand(o =>
            {
                CurrentView = ConvertVM;
            });

            EditViewCommand = new LambdaCommand(o =>
            {
                CurrentView = EditVM;
            });

            MergerViewCommand = new LambdaCommand(o =>
            {
                CurrentView = MergerVM;
            });

            CloseWindowCommand = new LambdaCommand(OnCloseWindowCommandExecuted, CanCloseWindowCommandExecute);
            ShareWindowCommand = new LambdaCommand(OnShareWindowCommandExecuted, CanShareWindowCommandExecute);
            TurnWindowCommand = new LambdaCommand(OnTurnWindowCommandExecuted, CanTurnWindowCommandExecute);
            GitHubLinkCommand = new LambdaCommand(OnGitHubLinkCommandExecuted, CanGitHubLinkCommandExecute);
        }

        private bool CanCloseWindowCommandExecute(object p) => true;
        private void OnCloseWindowCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        private bool CanShareWindowCommandExecute(object p) => true;
        private void OnShareWindowCommandExecuted(object p)
        {
            Application.Current.MainWindow.WindowState = 
                Application.Current.MainWindow.WindowState != WindowState.Maximized ?
                WindowState.Maximized : WindowState.Normal;
        }

        private bool CanTurnWindowCommandExecute(object p) => true;
        private void OnTurnWindowCommandExecuted(object p)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private bool CanGitHubLinkCommandExecute(object p) => true;
        private void OnGitHubLinkCommandExecuted(object p)
        {
            try
            {
                IWebDriver driver = new ChromeDriver();
                driver.Navigate().GoToUrl(GITHUB_URL);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

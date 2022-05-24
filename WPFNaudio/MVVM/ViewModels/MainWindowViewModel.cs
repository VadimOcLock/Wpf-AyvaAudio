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
        public LambdaCommand HomeViewCommand { get; set; }
        public LambdaCommand ConvertViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public ConvertViewModel ConvertVM { get; set; }

        private string _Title = "AYVA Audio";

        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get { return _Title; }
            set 
            {
                //if (Equals(value, _Title)) return;
                //_Title = value;
                //OnPropertyChanged();

                Set(ref _Title, value);
            }
        }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            {
                Set(ref _currentView, value);
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

        private LambdaCommand _shareWindowCommand;

        public LambdaCommand ShareWindowCommand
        {
            get 
            {
                return _shareWindowCommand ??
                    (_shareWindowCommand = new LambdaCommand(obj =>
                    {
                        Window w = obj as Window;
                        if (w != null)
                        {
                            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
                            {
                                Application.Current.MainWindow.WindowState = WindowState.Maximized;
                            }
                            else
                            {
                                Application.Current.MainWindow.WindowState = WindowState.Normal;
                            }
                        }
                    }));
            }
        }

        private LambdaCommand _turnWindowCommand;

        public LambdaCommand TurnWindowCommand
        {
            get
            {
                return _turnWindowCommand ??
                    (_turnWindowCommand = new LambdaCommand(obj =>
                    {
                        Window w = obj as Window;
                        if (w != null)
                        {
                            Application.Current.MainWindow.WindowState = WindowState.Minimized;
                        }
                    }));
            }
        }

        private string gitHubUrl = "https://github.com/VadimOcLock";
        public LambdaCommand GitHubLinkCommand { get; }

        private bool CanGitHubLinkCommandExecute(object p) => true;
        private void OnGitHubLinkCommandExecuted(object p)
        {
            try
            {
                IWebDriver driver = new ChromeDriver();
                driver.Navigate().GoToUrl(gitHubUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public MainWindowViewModel()
        {
            HomeVM = new HomeViewModel();
            ConvertVM = new ConvertViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new LambdaCommand(o =>
            {
                CurrentView = HomeVM;
            });

            ConvertViewCommand = new LambdaCommand(o =>
            {
                CurrentView = ConvertVM;
            });

            GitHubLinkCommand = new LambdaCommand(OnGitHubLinkCommandExecuted, CanGitHubLinkCommandExecute);
        }
    }
}

using MarioProject.Models;
using MarioProject.ViewModels;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;


namespace MarioProject
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowVM MainWindowVM { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            MainWindowVM = new MainWindowVM(canvas, changeCharacterButton);
            DataContext = MainWindowVM;
        }
        private void backgroundMusic_MediaEnded(object sender, RoutedEventArgs e)
        {
            backgroundMusic.Position = TimeSpan.Zero;
            backgroundMusic.Play();
        }
    }
}

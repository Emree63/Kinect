using Microsoft.Kinect;
using Model;
using Model.Stream;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Media;
using KinectExercises.Stream;
using System.Windows.Controls;
using System.ComponentModel;
using KinectExercises.ViewModels;

namespace KinectExercises
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindowVM MainWindowVM { get; set; }

        public MainWindow()
        {
            MainWindowVM = new MainWindowVM();
            InitializeComponent();
            DataContext = this;
        }

    }
}
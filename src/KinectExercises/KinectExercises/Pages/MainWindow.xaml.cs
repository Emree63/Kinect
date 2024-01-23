using Microsoft.Kinect;
using Model;
using Model.Stream;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Media;
using KinectExercises.Stream;
using System.Windows.Controls;
using System.ComponentModel;

namespace KinectExercises
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private KinectStream? stream = null;
        private KinectManager manager = new();
        private KinectStreamFactory factory;

        void switchStream(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var enumValue = StreamType.None;
            Enum.TryParse(button.Tag.ToString(), out enumValue);
            stream = factory[enumValue];
            if (stream != null)
            {
                dataFlow.Source = stream.Source;
            }
        }


        public MainWindow()
        {
            factory = new(manager);
            InitializeComponent();
            DataContext = manager;
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            manager.StopSensor();
        }

    }
}
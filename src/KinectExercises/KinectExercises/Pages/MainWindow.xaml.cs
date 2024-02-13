using Microsoft.Kinect;
using Model;
using Model.Stream;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Media;
using KinectExercises.Stream;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Ink;
using Model.gesture;
using System.Diagnostics;

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

        private void testGesture(object sender, GestureRecognizedEventArgs e)
        {
            Debug.WriteLine("GESTURE !!!" + e.Gesture.GestureName);
        }

        public MainWindow()
        {
            factory = new(manager);
            InitializeComponent();
            DataContext = manager;

            GestureManager.AddGestures(new PostureRightHandUp());
            GestureManager.GestureRecognized += testGesture;

            GestureManager.StartAcquiringFrames(manager);
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            manager.StopSensor();
        }

    }
}
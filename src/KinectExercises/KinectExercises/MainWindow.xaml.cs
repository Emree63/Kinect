using Microsoft.Kinect;
using Model;
using Model.Stream;
using System.Diagnostics;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Media;
using System.IO;
using KinectExercises.Stream;
using System.Windows.Controls;

namespace KinectExercises
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private WriteableBitmap? bitmap = null;
        private KinectStream? stream = null;
        private KinectManager manager = new();

        void switchStream(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var enumValue = StreamType.None;
            Enum.TryParse(button.Tag.ToString(), out enumValue);
            switch (enumValue)
            {
                case StreamType.ColorStream:
                    FrameDescription colorFrameDescription = this.manager.Sensor.ColorFrameSource.CreateFrameDescription(ColorImageFormat.Rgba);
                    this.bitmap = new WriteableBitmap(colorFrameDescription.Width, colorFrameDescription.Height, 96, 96, PixelFormats.Bgra32, null);
                    dataFlow.Source = this.bitmap;
                    break;
                case StreamType.DepthStream:
                    FrameDescription depthFrameDescription = this.manager.Sensor.DepthFrameSource.FrameDescription;
                    this.bitmap  = new WriteableBitmap(depthFrameDescription.Width, depthFrameDescription.Height, 96, 96, PixelFormats.Bgra32, null);
                    dataFlow.Source = this.bitmap;
                    break;
                default:
                    // FrameDescription colorFrameDescription = this.manager.Sensor.ColorFrameSource.CreateFrameDescription(ColorImageFormat.Rgba);
                    // this.bitmap = new WriteableBitmap(colorFrameDescription.Width, colorFrameDescription.Height, 96, 96, PixelFormats.Bgra32, null);
                    break;
            }
            stream = KinectStreamFactory.BuildStream(manager, bitmap, enumValue);
        }


        public MainWindow()
        {

            // stream = new ColorImageStream(manager, bitmap);
            InitializeComponent();
            DataContext = manager;
            dataFlow.Source = this.bitmap;
        }
    }
}
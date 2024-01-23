using Microsoft.Kinect;
using Model;
using Model.Stream;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Media;
using KinectExercises.Stream;
using System.Windows.Controls;

namespace KinectExercises
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private KinectStream? stream = null;
        private KinectManager manager = new();

        void switchStream(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var enumValue = StreamType.None;
            Enum.TryParse(button.Tag.ToString(), out enumValue);
            stream = new KinectStreamFactory(manager)[enumValue];
            dataFlow.Source = stream.Source;
        }


        public MainWindow()
        {
            // stream = new ColorImageStream(manager, bitmap);
            InitializeComponent();
            DataContext = manager;
        }

    }
}
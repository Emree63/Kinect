using Model.gesture;
using System.Diagnostics;
using KinectExercises.ViewModels;

namespace KinectExercises
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindowVM MainWindowVM { get; set; }

        private void testGesture(object sender, GestureRecognizedEventArgs e)
        {
            Debug.WriteLine("GESTURE !!!" + e.Gesture.GestureName);
        }

        public MainWindow()
        {
            MainWindowVM = new();
            InitializeComponent();
            DataContext = MainWindowVM;

            GestureManager.AddGestures(
                new PostureOneHandUp(), 
                new PostureRightHand(), 
                new PostureLeftHand(),
                new PostureFireball()
                );
            GestureManager.GestureRecognized += testGesture;

            GestureManager.StartAcquiringFrames(MainWindowVM.Manager);
        }
    }
}
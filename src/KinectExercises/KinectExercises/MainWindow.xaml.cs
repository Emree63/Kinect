using Model.Stream;
using System.Diagnostics;

namespace KinectExercises
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public ColorImageStream stream { get; set; }

        public MainWindow()
        {
            stream = new ColorImageStream(new());

            InitializeComponent();
            DataContext = this;
        }
    }
}
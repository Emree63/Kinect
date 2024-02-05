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
            DataContext = MainWindowVM;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MarioProject
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool IsOpen { get; set; } = false;

        private bool isJumping = false;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private async void JumpButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isJumping)
            {
                isJumping = true;
                DoubleAnimation jumpAnimation = new DoubleAnimation();
                jumpAnimation.From = Canvas.GetBottom(marioImage);
                jumpAnimation.To = Canvas.GetBottom(marioImage) + 150;
                jumpAnimation.Duration = TimeSpan.FromSeconds(0.5);

                DoubleAnimation fallAnimation = new DoubleAnimation();
                fallAnimation.From = Canvas.GetBottom(marioImage) + 150;
                fallAnimation.To = Canvas.GetBottom(marioImage);
                fallAnimation.Duration = TimeSpan.FromSeconds(0.5);

                jumpAnimation.Completed += (s, _) =>
                {
                    marioImage.BeginAnimation(Canvas.BottomProperty, fallAnimation);
                };
                marioImage.BeginAnimation(Canvas.BottomProperty, jumpAnimation);
                await Task.Delay(TimeSpan.FromSeconds(1));
                isJumping = false;
            }
        }

        private void MoveLeftButton_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation moveAnimation = new DoubleAnimation();
            moveAnimation.From = Canvas.GetLeft(marioImage);
            moveAnimation.To = Canvas.GetLeft(marioImage) - 50;
            moveAnimation.Duration = TimeSpan.FromSeconds(0.5);

            marioImage.BeginAnimation(Canvas.LeftProperty, moveAnimation);

            ((ScaleTransform)marioImage.RenderTransform).ScaleX = -1;
        }

        private void MoveRightButton_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation moveAnimation = new DoubleAnimation();
            moveAnimation.From = Canvas.GetLeft(marioImage);
            moveAnimation.To = Canvas.GetLeft(marioImage) + 50;
            moveAnimation.Duration = TimeSpan.FromSeconds(0.5);

            marioImage.BeginAnimation(Canvas.LeftProperty, moveAnimation);

            ((ScaleTransform)marioImage.RenderTransform).ScaleX = 1;
        }


    }
}

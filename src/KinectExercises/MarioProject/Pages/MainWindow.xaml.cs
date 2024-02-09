using MarioProject.Models;
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
        private List<Character> characters = new List<Character>();
        private int currentCharacterIndex = 0;
        public bool IsOpen { get; set; } = false;

        private bool isJumping = false;

        public MainWindow()
        {
            InitializeComponent();
            LoadCharacters();
            UpdateCharacterButtonContent();
            DataContext = this;
        }

        private void LoadCharacters()
        {
            characters.Add(new Character("/Images/mario.png", 50, 100));
            characters.Add(new Character("/Images/luigi.png", 58, 100));
            characters.Add(new Character("/Images/waluigi.png", 50, 110));
        }

        private async void JumpButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isJumping)
            {
                isJumping = true;
                DoubleAnimation jumpAnimation = new DoubleAnimation();
                jumpAnimation.From = Canvas.GetBottom(mainCharacterImage);
                jumpAnimation.To = Canvas.GetBottom(mainCharacterImage) + 150;
                jumpAnimation.Duration = TimeSpan.FromSeconds(0.5);

                DoubleAnimation fallAnimation = new DoubleAnimation();
                fallAnimation.From = Canvas.GetBottom(mainCharacterImage) + 150;
                fallAnimation.To = Canvas.GetBottom(mainCharacterImage);
                fallAnimation.Duration = TimeSpan.FromSeconds(0.5);

                jumpAnimation.Completed += (s, _) =>
                {
                    mainCharacterImage.BeginAnimation(Canvas.BottomProperty, fallAnimation);
                };
                mainCharacterImage.BeginAnimation(Canvas.BottomProperty, jumpAnimation);
                await Task.Delay(TimeSpan.FromSeconds(1));
                isJumping = false;
            }
        }
        private void MoveLeftButton_Click(object sender, RoutedEventArgs e)
        {
            double newPosition = Canvas.GetLeft(mainCharacterImage) - 50;
            if (newPosition >= 30)
            {
                DoubleAnimation moveAnimation = new DoubleAnimation();
                moveAnimation.From = Canvas.GetLeft(mainCharacterImage);
                moveAnimation.To = newPosition;
                moveAnimation.Duration = TimeSpan.FromSeconds(0.35);

                mainCharacterImage.BeginAnimation(Canvas.LeftProperty, moveAnimation);

                ((ScaleTransform)mainCharacterImage.RenderTransform).ScaleX = -1;
            }
        }

        private void MoveRightButton_Click(object sender, RoutedEventArgs e)
        {
            double newPosition = Canvas.GetLeft(mainCharacterImage) + 50;
            if (newPosition + mainCharacterImage.ActualWidth + 50 <= canvas.ActualWidth)
            {
                DoubleAnimation moveAnimation = new DoubleAnimation();
                moveAnimation.From = Canvas.GetLeft(mainCharacterImage);
                moveAnimation.To = newPosition;
                moveAnimation.Duration = TimeSpan.FromSeconds(0.35);

                mainCharacterImage.BeginAnimation(Canvas.LeftProperty, moveAnimation);

                ((ScaleTransform)mainCharacterImage.RenderTransform).ScaleX = 1;
            }
        }

        public class Fireball
        {
            public Image Image { get; set; }
            public double Left { get; set; }
            public double Top { get; set; }
        }

        private List<Fireball> fireballs = new List<Fireball>();

        private void ThrowsFireball(object sender, RoutedEventArgs e)
        {
            Image fireballImage = new Image();
            fireballImage.Source = new BitmapImage(new Uri("/Images/fireball.png", UriKind.Relative));
            fireballImage.Stretch = Stretch.Fill;
            fireballImage.Width = 30;
            fireballImage.Height = 30;

            fireballImage.RenderTransformOrigin = new Point(0.5, 0.5);
            ScaleTransform flipTransform = new ScaleTransform(-1, 1);
            fireballImage.RenderTransform = flipTransform;

            Fireball fireball = new Fireball();
            fireball.Image = fireballImage;
            fireball.Left = canvas.ActualWidth - Canvas.GetRight(bowserImage) - 90;
            fireball.Top = canvas.ActualHeight - Canvas.GetBottom(bowserImage) - 100;
            fireballs.Add(fireball);

            canvas.Children.Add(fireball.Image);

            Canvas.SetLeft(fireball.Image, fireball.Left);
            Canvas.SetTop(fireball.Image, fireball.Top);

            DoubleAnimation fireballAnimation = new DoubleAnimation();
            fireballAnimation.From = fireball.Left;
            fireballAnimation.To = -50;
            fireballAnimation.Duration = TimeSpan.FromSeconds(2);

            fireballAnimation.Completed += (sender, e) =>
            {
                canvas.Children.Remove(fireball.Image);
                fireballs.Remove(fireball);
            };

            fireball.Image.BeginAnimation(Canvas.LeftProperty, fireballAnimation);
        }

        private void ChangeMainCharacterImage_Click(object sender, RoutedEventArgs e)
        {
            currentCharacterIndex++;
            if (currentCharacterIndex >= characters.Count)
            {
                currentCharacterIndex = 0;
            }
            UpdateCharacterButtonContent();
        }

        private void UpdateCharacterButtonContent()
        {
            int nextCharacterIndex = (currentCharacterIndex + 1) % characters.Count;

            Character nextCharacter = characters[nextCharacterIndex];
            Character currentCharacter = characters[currentCharacterIndex];

            Image nextCharacterButtonImage = new Image();
            nextCharacterButtonImage.Source = new BitmapImage(new Uri(nextCharacter.ImageUrl, UriKind.Relative));
            nextCharacterButtonImage.Width = 30;
            nextCharacterButtonImage.Height = 30;

            changeCharacterButton.Content = nextCharacterButtonImage;

            mainCharacterImage.Source = new BitmapImage(new Uri(currentCharacter.ImageUrl, UriKind.Relative));
            mainCharacterImage.Width = currentCharacter.Width;
            mainCharacterImage.Height = currentCharacter.Height;
            mainCharacterImage.Stretch = Stretch.Fill;
        }

    }
}

using MarioProject.Models;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;


namespace MarioProject
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private List<Character> characters = new List<Character>();
        private int currentCharacterIndex = 0;
        public bool IsOpen { get; set; } = false;
        private bool heCanPlay = false;
        private bool isJumping = false;
        private bool isJumpingBowser = false;
        private bool isTurn = false;

        private double health = 100;
        public double Health
        {
            get { return health; }
            set
            {
                health = value;
                OnPropertyChanged(nameof(Health));
            }
        }

        private double bowserHealth = 100;
        public double BowserHealth
        {
            get { return bowserHealth; }
            set
            {
                bowserHealth = value;
                OnPropertyChanged(nameof(BowserHealth));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindow()
        {
            InitializeComponent();
            backgroundMusic.Play();
            LoadCharacters();
            UpdateCharacterButtonContent();
            DataContext = this;
            Start();
        }

        private void backgroundMusic_MediaEnded(object sender, RoutedEventArgs e)
        {
            backgroundMusic.Position = TimeSpan.Zero;
            backgroundMusic.Play();
        }
        private void LoadCharacters()
        {
            characters.Add(new Character("/Images/mario.png", 50, 100, "/Images/fireball.png", 30, 30));
            characters.Add(new Character("/Images/luigi.png", 58, 100, "/Images/fireball_green.png", 30, 30));
            characters.Add(new Character("/Images/waluigi.png", 50, 110, "/Images/fireball_purple.png", 30, 30));
            characters.Add(new Character("/Images/link.png", 80, 100, "/Images/sword.png", 30, 20));
        }

        private async void JumpMainCharacter_Click(object sender, RoutedEventArgs e)
        {
            if (!isJumping && heCanPlay)
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
        private void MoveLeftMainCharacter_Click(object sender, RoutedEventArgs e)
        {
            if (heCanPlay)
            {
                double newPosition = Canvas.GetLeft(mainCharacterImage) - 50;
                if (newPosition >= 50)
                {
                    DoubleAnimation moveAnimation = new DoubleAnimation();
                    moveAnimation.From = Canvas.GetLeft(mainCharacterImage);
                    moveAnimation.To = newPosition;
                    moveAnimation.Duration = TimeSpan.FromSeconds(0.35);

                    mainCharacterImage.BeginAnimation(Canvas.LeftProperty, moveAnimation);

                    ((ScaleTransform)mainCharacterImage.RenderTransform).ScaleX = -1;
                    isTurn = true;
                    CheckCollisionWithBowser();
                }
            }
        }

        private void MoveRightMainCharacter_Click(object sender, RoutedEventArgs e)
        {
            if (heCanPlay)
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
                    isTurn = false;
                    CheckCollisionWithBowser();
                }
            }
        }

        private void MoveLeftBowser()
        {
            double newPosition = Canvas.GetRight(bowserImage) + 50; 
            if (newPosition + bowserImage.ActualWidth <= canvas.ActualWidth)
            {
                DoubleAnimation moveAnimation = new DoubleAnimation();
                moveAnimation.From = Canvas.GetRight(bowserImage);
                moveAnimation.To = newPosition;
                moveAnimation.Duration = TimeSpan.FromSeconds(0.35);

                bowserImage.BeginAnimation(Canvas.RightProperty, moveAnimation);
            }
        }

        private void MoveRightBowser()
        {

            double newPosition = Canvas.GetRight(bowserImage) - 50;
            if (newPosition >= 0)
            {
                DoubleAnimation moveAnimation = new DoubleAnimation();
                moveAnimation.From = Canvas.GetRight(bowserImage);
                moveAnimation.To = newPosition;
                moveAnimation.Duration = TimeSpan.FromSeconds(0.35);

                bowserImage.BeginAnimation(Canvas.RightProperty, moveAnimation);
            }
        }

        private async void JumpBowser()
        {
            if (!isJumpingBowser)
            {
                isJumpingBowser = true;
                DoubleAnimation jumpAnimation = new DoubleAnimation();
                jumpAnimation.From = Canvas.GetBottom(bowserImage);
                jumpAnimation.To = Canvas.GetBottom(bowserImage) + 150;
                jumpAnimation.Duration = TimeSpan.FromSeconds(0.5);

                DoubleAnimation fallAnimation = new DoubleAnimation();
                fallAnimation.From = Canvas.GetBottom(bowserImage) + 150;
                fallAnimation.To = Canvas.GetBottom(bowserImage);
                fallAnimation.Duration = TimeSpan.FromSeconds(0.5);

                jumpAnimation.Completed += (s, _) =>
                {
                    bowserImage.BeginAnimation(Canvas.BottomProperty, fallAnimation);
                };
                bowserImage.BeginAnimation(Canvas.BottomProperty, jumpAnimation);
                await Task.Delay(TimeSpan.FromSeconds(1));
                isJumpingBowser = false;
            }
        }

        private CancellationTokenSource cancellationTokenSource;

        private async void Start()
        {
            await Task.Delay(2000);
            heCanPlay = true;
            cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            Random random = new Random();

            while (heCanPlay)
            {
                Task jumpTask = JumpAsync(cancellationToken);
                Task fireballTask = ThrowFireballBowserAsync(cancellationToken);
                Task walkTask = WalkAsync(cancellationToken);

                await Task.WhenAll(jumpTask, fireballTask, walkTask);
            }
        }

        private async Task JumpAsync(CancellationToken cancellationToken)
        {
            Random random = new Random();
            while (!cancellationToken.IsCancellationRequested && heCanPlay)
            {
                await Task.Delay(random.Next(1000, 2501));
                if (!cancellationToken.IsCancellationRequested && heCanPlay)
                {
                    JumpBowser();
                }
            }
        }

        private async Task ThrowFireballBowserAsync(CancellationToken cancellationToken)
        {
            Random random = new Random();
            while (!cancellationToken.IsCancellationRequested && heCanPlay)
            {
                await Task.Delay(random.Next(200, 1801));
                if (!cancellationToken.IsCancellationRequested && heCanPlay)
                {
                    ThrowsFireballBowser();
                }
            }
        }

        private async Task WalkAsync(CancellationToken cancellationToken)
        {
            Random random = new Random();
            while (!cancellationToken.IsCancellationRequested && heCanPlay)
            {
                await Task.Delay(random.Next(1000, 2001));
                if (!cancellationToken.IsCancellationRequested && heCanPlay)
                {
                    MoveLeftBowser();
                    await Task.Delay(700);
                    MoveRightBowser();
                }
            }
        }
        public class Fireball
        {
            public Image Image { get; set; }
            public double Left { get; set; }
            public double Bottom { get; set; }
        }

        private void ThrowsFireball(object sender, RoutedEventArgs e)
        {
            if (heCanPlay)
            {
                Image fireballImage = new Image();
                fireballImage.Source = new BitmapImage(new Uri(characters[currentCharacterIndex].WeaponImage, UriKind.Relative));
                fireballImage.Stretch = Stretch.Fill;
                fireballImage.Width = characters[currentCharacterIndex].WidthWeapon;
                fireballImage.Height = characters[currentCharacterIndex].HeightWeapon;


                Fireball fireball = new Fireball();
                fireball.Image = fireballImage;
                if (isTurn)
                {
                    fireballImage.RenderTransformOrigin = new Point(0.5, 0.5);
                    ScaleTransform flipTransform = new ScaleTransform(-1, 1);
                    fireballImage.RenderTransform = flipTransform;
                    fireball.Left = Canvas.GetLeft(mainCharacterImage) - characters[currentCharacterIndex].Width;
                }
                else
                {
                    fireball.Left = Canvas.GetLeft(mainCharacterImage) + characters[currentCharacterIndex].Width;
                }
                fireball.Bottom = Canvas.GetBottom(mainCharacterImage) + (characters[currentCharacterIndex].Height / 2);

                canvas.Children.Add(fireball.Image);

                Canvas.SetLeft(fireball.Image, fireball.Left);
                Canvas.SetBottom(fireball.Image, fireball.Bottom);

                DoubleAnimation fireballAnimation = new DoubleAnimation();
                fireballAnimation.From = fireball.Left;
                if (isTurn)
                {
                    fireballAnimation.To = -1000;
                }
                else
                {
                    fireballAnimation.To = canvas.ActualWidth + 100;
                }
                fireballAnimation.Duration = TimeSpan.FromSeconds(2);

                fireballAnimation.Completed += (sender, e) =>
                {
                    canvas.Children.Remove(fireball.Image);
                };

                fireballAnimation.CurrentTimeInvalidated += (sender, e) =>
                {
                    CheckCollisionBowser(fireball);
                };

                fireball.Image.BeginAnimation(Canvas.LeftProperty, fireballAnimation);
            }
        }

        private void ThrowsFireballBowser()
        {
            Image fireballImage = new Image();
            fireballImage.Source = new BitmapImage(new Uri("/Images/fireball.png", UriKind.Relative));
            fireballImage.Stretch = Stretch.Fill;
            fireballImage.Width = 40;
            fireballImage.Height = 40;

            fireballImage.RenderTransformOrigin = new Point(0.5, 0.5);
            ScaleTransform flipTransform = new ScaleTransform(-1, 1);
            fireballImage.RenderTransform = flipTransform;

            Fireball fireball = new Fireball();
            fireball.Image = fireballImage;
            fireball.Left = canvas.ActualWidth - Canvas.GetRight(bowserImage) - 90;
            fireball.Bottom = Canvas.GetBottom(bowserImage) + 70;

            canvas.Children.Add(fireball.Image);

            Canvas.SetLeft(fireball.Image, fireball.Left);
            Canvas.SetBottom(fireball.Image, fireball.Bottom);

            DoubleAnimation fireballAnimation = new DoubleAnimation();
            fireballAnimation.From = fireball.Left;
            fireballAnimation.To = -100;
            fireballAnimation.Duration = TimeSpan.FromSeconds(2);

            fireballAnimation.Completed += (sender, e) =>
            {
                canvas.Children.Remove(fireball.Image);
            };

            fireballAnimation.CurrentTimeInvalidated += (sender, e) =>
            {
                CheckCollisionMainCharacter(fireball);
            };

            fireball.Image.BeginAnimation(Canvas.LeftProperty, fireballAnimation);
        }

        private async void CheckCollisionMainCharacter(Fireball fireball)
        {
            double fireballLeft = Canvas.GetLeft(fireball.Image);
            double fireballBottom = fireball.Bottom;

            double mainCharacterLeft = Canvas.GetLeft(mainCharacterImage);
            if (isTurn)
            {
                mainCharacterLeft -= characters[currentCharacterIndex].Width;
            }
            double mainCharacterRight = mainCharacterLeft + mainCharacterImage.ActualWidth;
            double mainCharacterTop = Canvas.GetBottom(mainCharacterImage);
            double mainCharacterBottom = mainCharacterTop + mainCharacterImage.ActualHeight;

            if ((fireballLeft >= mainCharacterLeft && fireballLeft <= mainCharacterRight) &&
                (fireballBottom >= mainCharacterTop && fireballBottom <= mainCharacterBottom) && heCanPlay)
            {
                canvas.Children.Remove(fireball.Image);

                ReduceHealth(1);
                mainCharacterImage.Effect = new System.Windows.Media.Effects.DropShadowEffect()
                {
                    Color = Colors.Red,
                    BlurRadius = 10,
                    ShadowDepth = 0,
                    Opacity = 1,
                };

                await Task.Delay(100);

                mainCharacterImage.Effect = null;
            }
        }

        private async void CheckCollisionBowser(Fireball fireball)
        {
            double fireballLeft = Canvas.GetLeft(fireball.Image);
            double fireballBottom = fireball.Bottom;

            double bowserRight = canvas.ActualWidth - Canvas.GetRight(bowserImage);
            double bowserLeft = bowserRight - bowserImage.ActualWidth;
            double bowserTop = Canvas.GetBottom(bowserImage);
            double bowserBottom = bowserTop + bowserImage.ActualHeight;

            if ((fireballLeft >= bowserLeft && fireballLeft <= bowserRight) &&
                (fireballBottom >= bowserTop && fireballBottom <= bowserBottom) && heCanPlay)
            {
                canvas.Children.Remove(fireball.Image);

                ReduceHealthBowser(0.1);
                bowserImage.Effect = new System.Windows.Media.Effects.DropShadowEffect()
                {
                    Color = Colors.Red,
                    BlurRadius = 10,
                    ShadowDepth = 0,
                    Opacity = 1,
                };

                await Task.Delay(100);

                bowserImage.Effect = null;
            }
        }

        private async void CheckCollisionWithBowser()
        {
            double mainCharacterLeft = Canvas.GetLeft(mainCharacterImage) + characters[currentCharacterIndex].Width;
            double mainCharacterTop = Canvas.GetBottom(mainCharacterImage);

            double bowserRight = canvas.ActualWidth - Canvas.GetRight(bowserImage);
            double bowserLeft = bowserRight - bowserImage.ActualWidth;
            double bowserTop = Canvas.GetBottom(bowserImage);
            double bowserBottom = bowserTop + bowserImage.ActualHeight;

            if ((mainCharacterLeft >= bowserLeft && mainCharacterLeft <= bowserRight) &&
                (mainCharacterTop >= bowserTop && mainCharacterTop <= bowserBottom))
            {
                ReduceHealth(100);
                mainCharacterImage.Effect = new System.Windows.Media.Effects.DropShadowEffect()
                {
                    Color = Colors.Red,
                    BlurRadius = 10,
                    ShadowDepth = 0,
                    Opacity = 1,
                };

                await Task.Delay(100);

                mainCharacterImage.Effect = null;
            }
        }

        private void ReduceHealth(double amount)
        {
            Health -= amount;
            if (Health <= 0)
            {
                heCanPlay = false;
                mainCharacterImage.RenderTransform = new RotateTransform(90, mainCharacterImage.Width / 2, mainCharacterImage.Height / 2);
                Canvas.SetBottom(mainCharacterImage, Canvas.GetBottom(mainCharacterImage) - (characters[currentCharacterIndex].Height / 2.5));
            }
        }

        private void ReduceHealthBowser(double amount)
        {
            BowserHealth -= amount;
            if (BowserHealth <= 0)
            {
                heCanPlay = false;
                bowserImage.RenderTransform = new RotateTransform(100, bowserImage.Width / 2, bowserImage.Height / 2);
                Canvas.SetBottom(bowserImage, 40);
            }
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

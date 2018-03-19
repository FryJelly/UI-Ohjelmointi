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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Snake
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>

    // Enum for movement direction
    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    
    public partial class Game : Window
    {
        DispatcherTimer timer;
        private Random rnd = new Random();

        private int difficulty = 20;
        private int score = 0;

        private Point startPosition = new Point(100, 100);
        private Point currentPosition = new Point();
        private Size snakeSize = new Size(10, 10);
        private List<Point> snakeParts = new List<Point>();

        private Direction lastDirection = Direction.Right;
        private Direction currentDirection = Direction.Right;

        private int snakeLength = 100;


        public Game()
        {
            InitializeComponent();

            // init game timer
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(GameTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, difficulty);

            // Get keypresses to our OnKeyDown method
            this.KeyDown += new KeyEventHandler(OnKeyDown);

            currentPosition = startPosition;
            DrawSnake(currentPosition);

            //start the game
            timer.Start();
            
        }
        private void GameTick(object sender, EventArgs e)
        {
            switch (currentDirection)
            {
                case Direction.Down:
                    currentPosition.Y++;
                    break;

                case Direction.Up:
                    currentPosition.Y--;
                    break;

                case Direction.Left:
                    currentPosition.X--;
                    break;

                case Direction.Right:
                    currentPosition.X++;
                    break;
            }

            // check hits to game area
            if (currentPosition.X < 0 || currentPosition.X > GameCanvas.ActualWidth || currentPosition.Y < 0 || currentPosition.Y > GameCanvas.ActualHeight)
            {
                GameOver();
            }

            //check if snake hits itself
            for (int i = 0; i < (snakeParts.Count - snakeSize.Width * 2); i++)
            {
                if ((Math.Abs(snakeParts[i].X - currentPosition.X) < (snakeSize.Width)) &&
                    (Math.Abs(snakeParts[i].Y - currentPosition.Y) < (snakeSize.Width)))
                {
                    GameOver();
                    break;
                }
            }

            DrawSnake(currentPosition);
        }

        private void DrawSnake(Point position)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Fill = Brushes.Green;
            ellipse.Width = snakeSize.Width;
            ellipse.Height = snakeSize.Height;

            Canvas.SetTop(ellipse, position.Y);
            Canvas.SetLeft(ellipse, position.X);

            int count = GameCanvas.Children.Count;
            GameCanvas.Children.Add(ellipse);
            snakeParts.Add(position);

            if (count > snakeLength)
            {
                GameCanvas.Children.RemoveAt(0);
                snakeParts.RemoveAt(0);
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                    if (lastDirection != Direction.Up)
                    {
                        currentDirection = Direction.Down;
                    }
                    break;

                case Key.Up:
                    if (lastDirection != Direction.Down)
                    {
                        currentDirection = Direction.Up;
                    }
                    break;

                case Key.Left:
                    if (lastDirection != Direction.Right)
                    {
                        currentDirection = Direction.Left;
                    }
                    break;

                case Key.Right:
                    if (lastDirection != Direction.Left)
                    {
                        currentDirection = Direction.Right;
                    }
                    break;

                case Key.Escape:
                    Close();
                    break;
            }
            lastDirection = currentDirection;
        }
        private void GameOver()
        {
            timer.Stop();

            MessageBox.Show("Your Score: " + score);
            Close();
        }
    }
}

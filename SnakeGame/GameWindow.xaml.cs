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

namespace SnakeGame
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private DispatcherTimer timer;
        DrawingGroup backingStore = new DrawingGroup();
        private Random rnd = new Random();

        private int tickInterval = 15;
        private int score = 0;

        // store our game window area size
        // public and static so game objects can easily access play area size
        public static int windowWidth = 0;
        public static int windowHeight = 0;

        private List<IGameObject> gameObjects = new List<IGameObject>();

        public GameWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // get our game window area
            FrameworkElement client = this.Content as FrameworkElement;
            windowWidth = (int)client.ActualWidth;
            windowHeight = (int)client.ActualHeight;

            // init game main loop timer
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(GameTick);
            timer.Interval = TimeSpan.FromMilliseconds(tickInterval);

            // get keypresses to our OnKeyDown method
            this.KeyDown += new KeyEventHandler(OnKeyDown);

            // init the snake and add to game objects list
            gameObjects.Add(new Snake());

            //init startup bonuses
            InitBonuses(12);
            InitSpeed(1);

            // start the game
            timer.Start();
        }

        private void InitBonuses(int count)
        {
            for (int i=0; i < count; i++)
            {
                Bonus bonus = new Bonus();
                SuperBonus superBonus = new SuperBonus();
                bonus.Radius = 10;
                superBonus.Radius = 11;
                bonus.Position = new Point(
                    rnd.Next(bonus.Radius, windowWidth - bonus.Radius),
                    rnd.Next(bonus.Radius, windowHeight - bonus.Radius));
                superBonus.Position = new Point(
                    rnd.Next(superBonus.Radius, windowWidth - superBonus.Radius),
                    rnd.Next(superBonus.Radius, windowHeight - superBonus.Radius));

                gameObjects.Add(bonus);
                gameObjects.Add(superBonus);
            }
        }
        private void InitSpeed(int count)
        {
            for (int i = 0; i < count; i++)
            {
                SpeedBonus speedBonus = new SpeedBonus();
                speedBonus.Radius = 10;
                speedBonus.Position = new Point(
                    rnd.Next(speedBonus.Radius, windowWidth - speedBonus.Radius),
                    rnd.Next(speedBonus.Radius, windowHeight - speedBonus.Radius));

                gameObjects.Add(speedBonus);
            }
        }

        private void GameTick(object sender, EventArgs e)
        {
            Snake snake = gameObjects[0] as Snake;

            // update all game objects
            Bonus hitBonus = null;
            SuperBonus hitSuper = null;
            SpeedBonus hitSpeed = null;
            foreach (IGameObject gameObject in gameObjects)
            {
                gameObject.Update();

                // check hits to bonuses
                Bonus bonus = gameObject as Bonus;
                SuperBonus superBonus = gameObject as SuperBonus;
                SpeedBonus speedBonus = gameObject as SpeedBonus;
                if (bonus != null)
                {
                    if((Math.Abs(bonus.Position.X - snake.Position.X) < bonus.Radius + snake.Radius) &&
                        (Math.Abs(bonus.Position.Y - snake.Position.Y) < bonus.Radius + snake.Radius))
                    {
                        // its a hit!
                        // store the bonus that hit with the snake
                        hitBonus = bonus;
                        
                    }
                }
                if (superBonus != null)
                {
                    if ((Math.Abs(superBonus.Position.X - snake.Position.X) < superBonus.Radius + snake.Radius) &&
                        (Math.Abs(superBonus.Position.Y - snake.Position.Y) < superBonus.Radius + snake.Radius))
                    {
                        // its a hit!
                        // store the bonus that hit with the snake
                        hitSuper = superBonus;

                    }
                }
                if (speedBonus != null)
                {
                    if ((Math.Abs(speedBonus.Position.X - snake.Position.X) < speedBonus.Radius + snake.Radius) &&
                        (Math.Abs(speedBonus.Position.Y - snake.Position.Y) < speedBonus.Radius + snake.Radius))
                    {
                        // its a hit!
                        // store the bonus that hit with the snake
                        hitSpeed = speedBonus;

                    }
                }
            }

            if (hitBonus != null)
            {
                gameObjects.Remove(hitBonus);
                score++;
                scoreText.Text = "SCORE: " + score;
                snake.IncLength(10);

                if (gameObjects.Count < 10)
                {
                    InitBonuses(5);
                    InitSpeed(1);
                }
            }
            if (hitSuper != null)
            {
                gameObjects.Remove(hitSuper);
                score += 10;
                scoreText.Text = "SCORE: " + score;
                snake.IncLength(15);

                if (gameObjects.Count < 10)
                {
                    InitBonuses(5);
                    InitSpeed(1);
                }
            }
            if (hitSpeed != null)
            {
                gameObjects.Remove(hitSpeed);
                score += 50;
                scoreText.Text = "SCORE: " + score;

                snake.IncSpeed(1);
                snake.IncLength(10);

                if (gameObjects.Count < 10)
                {
                    InitSpeed(1);
                }
            }

            // check game over condition
            if (snake.Position.X < snake.Radius || snake.Position.X > windowWidth - snake.Radius ||
                snake.Position.Y < snake.Radius || snake.Position.Y > windowHeight - snake.Radius ||
                snake.HitsItself())
            {
                GameOver();
            }


            // draw game
            var drawingContext = backingStore.Open();
            Render(drawingContext);
            drawingContext.Close();
        }

        private void Render(DrawingContext drawingContext)
        {
            // fill background to black
            Rect backgroundArea = new Rect(0, 0, windowWidth, windowHeight);
            drawingContext.DrawRectangle(Brushes.Black, null, backgroundArea);

            // draw all game objects
            foreach (IGameObject gameObject in gameObjects)
            {
                gameObject.Draw(drawingContext);
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            // draw our game backing store
            drawingContext.DrawDrawing(backingStore);
        }


        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            Snake snake = gameObjects[0] as Snake;
            if (snake != null)
            {
                // read keyboard input and instruct snake to turn
                switch (e.Key)
                {
                    case Key.Down:
                        snake.Turn(Snake.Direction.Down);
                        break;
                    case Key.Up:
                        snake.Turn(Snake.Direction.Up);
                        break;
                    case Key.Left:
                        snake.Turn(Snake.Direction.Left);
                        break;
                    case Key.Right:
                        snake.Turn(Snake.Direction.Right);
                        break;

                    case Key.Escape:
                        Close();
                        break;
                }
            }
        }

        private void GameOver()
        {
            timer.Stop();
            MessageBox.Show("Your Score: " + score);
            Close();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // get our game window area
            FrameworkElement client = this.Content as FrameworkElement;
            windowWidth = (int)client.ActualWidth;
            windowHeight = (int)client.ActualHeight;
        }
    }
}



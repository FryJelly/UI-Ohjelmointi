using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SnakeGame
{
    class Snake : IGameObject
    {
        // snake movement direction enum
        public enum Direction
        {
            Up,
            Right,
            Down,
            Left
        };

        public Point Position
        {
            get { return currentPosition; }
            set { currentPosition = value; }
        }

        public int Radius { get; set; }
        public int Speed { get; set; }

        public Direction CurrentDirection { get; set; }

        public Snake()
        {
            CurrentDirection = Direction.Right;
            Radius = 10;
            Speed = 2;
        }

        public void Turn(Direction dir)
        {
            // prevent snake from turning back to itself
            if (dir == Direction.Up && CurrentDirection != Direction.Down)
            {
                CurrentDirection = dir;
            }
            else if (dir == Direction.Down && CurrentDirection != Direction.Up)
            {
                CurrentDirection = dir;
            }
            else if (dir == Direction.Left && CurrentDirection != Direction.Right)
            {
                CurrentDirection = dir;
            }
            else if (dir == Direction.Right && CurrentDirection != Direction.Left)
            {
                CurrentDirection = dir;
            }
        }

        public void IncLength(int amount)
        {
            // increase length of the snake
            snakeLength += amount;
        }

        public void IncSpeed(int amount)
        {
            // increase speed of the snake
            Speed += amount;
        }

        public bool HitsItself()
        {
            // check if snake hits itself
            for (int i=0; i<(snakeParts.Count - Radius * 2); i++)
            {
                if ((Math.Abs(snakeParts[i].X - currentPosition.X) < (Radius)) &&
                    (Math.Abs(snakeParts[i].Y - currentPosition.Y) < (Radius)))
                {
                    return true;
                }
            }

            return false;
        }


        public void Update()
        {
            // move snake per current direction
            switch (CurrentDirection)
            {
                case Direction.Down:
                    currentPosition.Y += Speed;
                    break;
                case Direction.Up:
                    currentPosition.Y -= Speed;
                    break;
                case Direction.Left:
                    currentPosition.X -= Speed;
                    break;
                case Direction.Right:
                    currentPosition.X += Speed;
                    break;
            }

            snakeParts.Add(currentPosition);
            if (snakeParts.Count > snakeLength)
            {
                // remove the 'oldest' parts of snake tail if it is too long
                snakeParts.RemoveAt(0);
            }
        }

        public void Draw(DrawingContext drawingContext)
        {
            // draw all snake parts
            for (int i = 0; i < snakeParts.Count; i++)
            {
                drawingContext.DrawEllipse(
                    Brushes.Green,
                    null,
                    snakeParts[i],
                    Radius,
                    Radius);
            }
        }

        private Point currentPosition = new Point(100, 100);
        private List<Point> snakeParts = new List<Point>();
        private int snakeLength = 100;
    }
}

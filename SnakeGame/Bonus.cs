using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SnakeGame
{
    class Bonus : IGameObject
    {
        public Point Position { get; set; }

        public int Radius { get; set; }

        public void Update()
        {

        }

        public void Draw(DrawingContext drawingContext)
        {
            drawingContext.DrawEllipse(
                Brushes.Red,
                null,
                Position,
                Radius,
                Radius);
            
        }

    }
    class SuperBonus : IGameObject
    {
        public Point Position { get; set; }

        public int Radius { get; set; }

        public void Update()
        {

        }

        public void Draw(DrawingContext drawingContext)
        {
            drawingContext.DrawEllipse(
                Brushes.Yellow,
                null,
                Position,
                Radius,
                Radius);

        }

    }
    class SpeedBonus : IGameObject
    {
        public Point Position { get; set; }

        public int Radius { get; set; }

        public void Update()
        {

        }

        public void Draw(DrawingContext drawingContext)
        {
            drawingContext.DrawEllipse(
                Brushes.LightGreen,
                null,
                Position,
                Radius,
                Radius);

        }

    }
}

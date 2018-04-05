using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SnakeGame
{
    // common interface for all game objects
    interface IGameObject
    {
        Point Position { get; set; }

        int Radius { get; set; }

        void Update();

        void Draw(DrawingContext drawingContext);
    }
}


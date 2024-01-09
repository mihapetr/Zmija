using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Zmijica
{
    
    internal class SnakeProto
    {
        public Point position;
        private Game game;

        public SnakeProto(Game game, int x, int y)
        {
            position = new Point(x, y);
        }

        public List<Point> pos()
        {
            return new List<Point>() { position };
        }

        public void update(Point dir)
        {
            position.X += dir.X;
            position.Y += dir.Y;
        }
    }
}

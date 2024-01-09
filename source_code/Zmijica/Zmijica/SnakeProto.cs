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

        public List<Point> draw()
        {
            return new List<Point>() { position };
        }

        public void update()
        {
            position.X += 1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Zmijica
{
    internal class Snake
    {
        //koristim List i nadam se da tu zadrzi poredak elemenata(first je glava, last je rep)
        private List<Point> position
        {
            get;
            set;
        }

        public Point direction
        {
            get;
            set;
        }

        public Snake(int x = 3, int y = 3)
        {
            position = new List<Point>();
            //position.Insert(0, new Point(x, y-1));
            position.Insert(0, new Point(x, y));
            direction = new Point(0, 0);
        }

        public List<Point> getPosition()
        {
            return position;
        }

        public Point headPosition()
        {
            return position.First();
        }
        public Point tailPosition()
        {
            return position.Last();
        }

        //da se ovo napravi kak treba treba prvo napravit enum za vrste hrane pa prema tome slozit update
        public void update(Point newDir, Food food = Food.noFood)
        {
            if (newDir.X == 0 && newDir.Y == 0) return;

            //za pocetak moze samo genericno, dohvati glavu i smjer i dodaj prikladni element, makni zadnji
            Point newHeadPosition = this.headPosition();
            newHeadPosition.X += newDir.X;
            newHeadPosition.Y += newDir.Y;

            switch (food)
            {
                case Food.noFood:
                    position.Insert(0, newHeadPosition);
                    position.RemoveAt(position.Count - 1);
                    break;
                case Food.standard:
                    position.Insert(0, newHeadPosition);
                    break;
                default:
                    break;
            }
        }
    }
}

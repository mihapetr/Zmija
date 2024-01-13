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
        string name = "Luigi";
        int width;
        //koristim List i nadam se da tu zadrzi poredak elemenata(first je glava, last je rep)
        private List<Point> position
        {
            get;
            set;
        }

        public int Length
        {
            get { return position.Count; }
        }


        public Point direction
        {
            get;
            set;
        }

        public Snake(int width, int x = 3, int y = 3)
        {
            this.width = width;
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
        public void update(Point newDir, Food food = Food.noFood, int damage = 0)
        {
            if (newDir.X == 0 && newDir.Y == 0) return;

            //za pocetak moze samo genericno, dohvati glavu i smjer i dodaj prikladni element, makni zadnji
            Point newHeadPosition = this.headPosition();
            newHeadPosition.X += newDir.X;
            newHeadPosition.Y += newDir.Y;

            if (newHeadPosition.X == -1) newHeadPosition.X = width - 1;
            if (newHeadPosition.Y == -1) newHeadPosition.Y = width - 1;
            if (newHeadPosition.X == width) newHeadPosition.X = 0;
            if (newHeadPosition.Y == width) newHeadPosition.Y = 0;

            switch (food)
            {
                case Food.standard:
                    position.Insert(0, newHeadPosition);
                    break;
                case Food.poison:
                    position.Insert(0, newHeadPosition);
                    for (int i = 0; i < damage; i++)
                    {
                        if (position.Count == 1) break;
                        position.RemoveAt(position.Count - 1);

                    }
                    break;
                default:
                    position.Insert(0, newHeadPosition);
                    position.RemoveAt(position.Count - 1);
                    break;
            }
        }
    }
}

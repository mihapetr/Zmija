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
        Random r = new Random();
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
        //vraca int zbog random hrane, ako zmija moze proci kroz sebe vrati 1(pozitivno), ako se krece za dva mjesta vrati 2(negativno)
        //inace vrati 0 (nebitno)
        public int update(Point newDir, Food food = Food.noFood, int damage = 0)
        {
            if (newDir.X == 0 && newDir.Y == 0) return 0;

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
                case Food.random:
                    position.Insert(0, newHeadPosition);
                    position.RemoveAt(position.Count - 1);
                    //uvest random 70/30 sansu da
                    //ili zmija moze proc sama kroz sebe
                    int odds = r.Next(0, 100);
                    if(odds < 70) return 1;
                    //ili zmija se krece za dva polja
                    else return 2;
                default:
                    position.Insert(0, newHeadPosition);
                    position.RemoveAt(position.Count - 1);
                    break;
            }
            return 0;
        }
    }
}

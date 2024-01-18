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
        int width;
        Random r = new Random();
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
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="width"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Snake(int width, int x = 3, int y = 3)
        {
            this.width = width;
            position = new List<Point>();
            //position.Insert(0, new Point(x, y-1));
            position.Insert(0, new Point(x, y));
            direction = new Point(0, 0);
        }
        /// <summary>
        /// Vraca pozicije koje zmija zauzima
        /// </summary>
        public List<Point> getPosition()
        {
            return position;
        }
        /// <summary>
        /// Vraca pozicije glave zmije
        /// </summary>
        public Point headPosition()
        {
            return position.First();
        }
        /// <summary>
        /// Vraca pozicije repa zmije (zadmnju poziciju)
        /// </summary>
        public Point tailPosition()
        {
            return position.Last();
        }
        /// <summary>
        /// Azurira poziciju zmije za jedan otkucaj
        /// </summary>
        /// <param name="newDir"></param>
        /// <param name="food"></param>
        /// <param name="damage"></param>
        /// <returns>
        /// 1 ako je zmija pojela random hranu i uzinak je bio pozitivan
        /// 2 ako je zmija pojela random hranu i uzinak je bio negativan
        /// 0 inace
        /// </returns>
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
                    int odds = r.Next(0, 100);
                    //ili zmija moze proc sama kroz sebe
                    if (odds < 70) return 1;
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

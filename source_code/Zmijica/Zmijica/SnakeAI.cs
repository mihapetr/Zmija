using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zmijica
{
    internal class SnakeAI
    {
        string name = "Fuigi";
        int width;
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

        public SnakeAI(int width, int x = 3, int y = 3)
        {
            this.width = width;
            position = new List<Point>();
            //position.Insert(0, new Point(x, y-1));
            position.Insert(0, new Point(width - (x+1), width - (y + 1)));
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
        public int update(Point foodPos, List<Point> snakePosition, List<Point> transparentPoints, Food food = Food.noFood)
        {
            //TODO umjetna inteligencija za novi smjer
            Point newDir = newDirection(foodPos, snakePosition, transparentPoints);
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
                default:
                    position.Insert(0, newHeadPosition);
                    position.RemoveAt(position.Count - 1);
                    break;
            }
            return 0;
        }

        public Point newDirection(Point foodPos, List<Point> snakePosition, List<Point> transparentPoints)
        {
            Point newDir = new Point(0,0);
            Point head = headPosition();
            //napravi dva kruga po svim mogucnostima, prvi uz prioritet da se ide prema hrani, drugi da se ide na legalno
            if(foodPos.X > head.X && isLegalDirection(new Point(1, 0), snakePosition, transparentPoints))
            {
                newDir.X = 1;
            }
            else if (foodPos.X < head.X && isLegalDirection(new Point(-1, 0), snakePosition, transparentPoints))
            {
                newDir.X = -1;
            }
            else if (foodPos.Y > head.Y && isLegalDirection(new Point(0, 1), snakePosition, transparentPoints))
            {
                newDir.Y = 1;
            }
            else if (foodPos.Y < head.Y && isLegalDirection(new Point(0, -1), snakePosition, transparentPoints))
            {
                newDir.Y = -1;
            }
            else if (isLegalDirection(new Point(1, 0), snakePosition, transparentPoints))
            {
                newDir.X = 1;
            }
            else if (isLegalDirection(new Point(-1, 0), snakePosition, transparentPoints))
            {
                newDir.X = -1;
            }
            else if (isLegalDirection(new Point(0, 1), snakePosition, transparentPoints))
            {
                newDir.Y = 1;
            }
            else if (isLegalDirection(new Point(0, -1), snakePosition, transparentPoints))
            {
                newDir.Y = -1;
            }
            //if(newDir.X == 0 && newDir.Y == 0) return new Point(0, -1);
            return newDir;
        }
        private bool isLegalDirection(Point direction, List<Point> snakePosition, List<Point> transparentPoints)
        {
            Point head = headPosition();
            head.X += direction.X;
            head.Y += direction.Y;

            bool legalPosition = true;

            foreach(Point snake in snakePosition)
            {
                if(head == snake)
                {
                    legalPosition = false;
                }
            }
            List<Point> snakeAIPosition = getPosition();
            foreach (Point snakeAI in snakeAIPosition)
            {
                if (head == snakeAI)
                {
                    legalPosition = false;
                }
            }
            foreach (Point transparentPoint in transparentPoints)
            {
                if (head == transparentPoint)
                {
                    legalPosition = true;
                }
            }

            return legalPosition;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zmijica
{
    internal class Game : GameForm
    { 
        /// <summary>
        /// Kako bismo osigurali funkcionalnost forme, na početku se 
        /// poziva <see cref="GameForm.GameForm"/>.
        /// </summary>
        public Game() : base() 
        {
            timestamp = 0;
            foodPosition = new List<Tuple<Point, Food>>();
            Tuple<Point, Food> newFood = new Tuple <Point, Food>(new Point(7, 7), Food.standard);
            foodPosition.Insert(0, newFood);
        }

        /*
        U testnom primjeru zmija kreće s (1,1) i kreće se u desno. Ako izađe izvan ekrana se program ruši zbog
        exceptiona. Postavljanjem FPS se mijenja brzina. Strelicama na tipkovnici se kontrolira smjer kretanja.
        */

        // test data
        Snake snake;   // testna zmijica
        List<Tuple<Point, Food>> foodPosition;
        Point direction = new Point(0,0);    // testne kontrole //!mislim da ne treba(init smjer zmijice na 0,0)
        List<Point> pts = new List<Point>();    // sadrži koordinate zida
        int timestamp;

        string[] wallString = new string[] {
            "##########",
            "#........#",
            "#........#",
            "#........#",
            "#........#",
            "#........#",
            "#........#",
            "#........#",
            "#........#",
            "##########"
        };

        public override void Setup() 
        {
            InitializeScreen(10);   // za rezoluciju ekrana
            FPS = 2;    // postavljanje osvježavanja

            snake = new Snake(); // nova zmijica na (5,5)
            DrawList(snake.getPosition(), Color.Green); // inicijalno crtanje zmije

            // dekodiranje testnog zida
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if(wallString[j][i] == '#') pts.Add(new Point(i, j));
                }
            }
            DrawList(pts, Color.Purple);  // crta točke iz pts
        }

        //nez jel ovo treba bit neki override
        private void updateGame()
        {
            //TODO: generiraj hranu(dok nije u koliziji sa zmijom ili nekom hranom koja postoji)

            //provjeri je li zmija u koliziji s hranom
            Point headPosition = snake.headPosition();
            headPosition.X += direction.X;
            headPosition.Y += direction.Y;
            bool hasEaten = false;
            foreach (Tuple<Point, Food> food in foodPosition.ToList())
            {
                if(food.Item1 == headPosition)
                {
                    snake.update(direction, food.Item2);
                    foodPosition.Remove(food);
                    hasEaten = true;
                }
            }



            // crtanje zmije
            if (!hasEaten)
            {
                snake.update(direction);    // kontrola zmije
                //foodPosition.RemoveAll(item => item.Item1 == headPosition);
            }
        }

        public override void Draw() 
        {
            timestamp++;

            // resetiranje pozadine
            ClearScreen();
            DrawList(pts, Color.Purple);  // crta točke iz pts

            updateGame();
            //TODO mora se nacrtat svaka vrsta hrane pojedinacno
            List<Point> standardFood = new List<Point>();
            foreach(Tuple<Point,Food> food in foodPosition)
            {
                if(food.Item2 == Food.standard) standardFood.Insert(0, food.Item1);
            }
            DrawList(standardFood, Color.Red);

            DrawList(snake.getPosition(), Color.Green);
        }

        public override void KeyPressed() 
        {
            switch (KeyCode)
            {
                case Keys.Up:
                    direction.Y = -1;
                    direction.X = 0;
                    break;

                case Keys.Down:
                    direction.Y = 1;
                    direction.X = 0;
                    break;

                case Keys.Left:
                    direction.Y = 0;
                    direction.X = -1;
                    break;

                case Keys.Right:
                    direction.Y = 0;
                    direction.X = 1;
                    break;
            }

            
        }
    }
}

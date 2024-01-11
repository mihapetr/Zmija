﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zmijica
{

    public class Varijable
    {
        public int width;
    }

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
        List<Point> walls = new List<Point>();    // sadrži koordinate zida
        int timestamp;
        Random r = new Random();

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
            Console.WriteLine(varijable.width);
            InitializeScreen(10);   // za rezoluciju ekrana
            FPS = 2;    // postavljanje osvježavanja

            snake = new Snake(); // nova zmijica na (5,5)
            DrawList(snake.getPosition(), Color.Green); // inicijalno crtanje zmije

            // dekodiranje testnog zida
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if(wallString[j][i] == '#') walls.Add(new Point(i, j));
                }
            }
            DrawList(walls, Color.Purple);  // crta točke iz walls
        }

        //nez jel ovo treba bit neki override
        private void updateGame()
        {
            timestamp++;

            //gdje ce se pomaknut u sljedecem otkucaju
            Point headPosition = snake.headPosition();
            headPosition.X += direction.X;
            headPosition.Y += direction.Y;

            //provjeri je li zmija u koliziji sama sa sobom
            bool isDead = false;
            List<Point> snakePosition = snake.getPosition();
            foreach (Point snakeBody in snakePosition.ToList())
            {
                if (snakeBody == headPosition)
                {
                    isDead = true;
                }
            }
            if (isDead == true)
            {
                //TODO zavrsi igru 
                direction.X = 0;
                direction.Y = 0;
            }


            //provjeri je li zmija u koliziji s hranom
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
            }

            //generiraj otrov(ne smije bit u koliziji sa zidom, sa zmijom ili s drugim hranama
            //TODO poisonInterval bolje nego 5
            if (timestamp % 5 == 0)
            {
                //generiraj dok se ne nade dozvoljena pozicija
                while (true)
                {
                    bool isLegalPosition = true;
                    Point newPoison = new Point(r.Next(0, varijable.width - 1), r.Next(0, varijable.width - 1));
                    //kolizija sa zmijom?
                    foreach (Point snakeBody in snakePosition.ToList())
                    {
                        if (snakeBody == newPoison)
                        {
                            isLegalPosition = false;
                        }
                    }
                    if (headPosition == newPoison) isLegalPosition = false;

                    //kolizija sa hranom?
                    foreach (Tuple<Point, Food> food in foodPosition.ToList())
                    {
                        if (food.Item1 == newPoison)
                        {
                            isLegalPosition = false;
                            break;
                        }
                    }

                    //kolizija sa zidom?
                    foreach (Point wall in walls.ToList())
                    {
                        if (wall == newPoison)
                        {
                            isLegalPosition = false;
                        }
                    }

                    if (isLegalPosition)
                    {
                        Tuple <Point, Food> newFood = new Tuple<Point, Food>(newPoison, Food.poison);
                        foodPosition.Insert(0, newFood);
                        break;
                    }
                }
            }
        }

        public override void Draw() 
        {
            timestamp++;

            // resetiranje pozadine
            ClearScreen();
            DrawList(walls, Color.Purple);  // crta točke iz walls

            updateGame();
            // nacrtaj standardFood
            List<Point> standardFood = new List<Point>();
            foreach(Tuple<Point,Food> food in foodPosition)
            {
                if(food.Item2 == Food.standard) standardFood.Insert(0, food.Item1);
            }
            //DrawList(standardFood, Color.Yellow);
            // nacrtaj poisonFood
            List<Point> poisonFood = new List<Point>();
            foreach (Tuple<Point, Food> food in foodPosition)
            {
                if (food.Item2 == Food.poison) poisonFood.Insert(0, food.Item1);
            }
            DrawList(poisonFood, Color.Red);

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

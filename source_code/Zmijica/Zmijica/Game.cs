﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zmijica
{

    /// <summary>
    /// Meta varijable. Svaka instanca Game ima svoju instancu.
    /// </summary>
    public class Varijable
    {
        // tehnički detalji
        public int width = 10;
        public bool snakeAlive = true;

        // težina igre
        public int goalLength = 3;
        public int poisonInterval = 20;
        public int poisonDamage = 3;
        public int FPS = 5;

        // score screen
        public int score = 30;
        public int stage = 1;
        public int level = 1;
    }

    internal class Game : GameForm
    {
        #region Konstruktor 

        /// <summary>
        /// Kako bismo osigurali funkcionalnost forme, na početku se 
        /// poziva <see cref="GameForm.GameForm"/>.
        /// </summary>
        public Game() : base() {}   // ovdje se ništa ne upisuje

        #endregion

        // ---------------------------- VARIJABLE -------------------------------

        List<Point> walls = new List<Point>();    // sadrži koordinate zida
        Stage[] stage = new Stage[4];

        Snake snake;
        List<Tuple<Point, Food>> foodPosition;
        Point direction = new Point(0,0);    // testne kontrole //!mislim da ne treba(init smjer zmijice na 0,0)
        Point newDirection = new Point(0, 0);    // testne kontrole //!mislim da ne treba(init smjer zmijice na 0,0)
        Tuple<Point, Food> newFood;
        ulong timestamp;
        Random r = new Random();

        // -------------------------- TESTNE VARIJABLE ---------------------------
        // ovamo stvari koje se testiraju



        // ----------------------- Setup, Draw, KeyPressed ----------------------------------

        public override void Setup() 
        {
            // inicijalizacija svih polja za igru
            // tako osiguravamo brzi prelazak
            stage[3] = new Stage(3);
            GetScreen(stage[3].width);   // za rezoluciju ekrana

            stage[2] = new Stage(2);
            GetScreen(stage[2].width);

            stage[1] = new Stage(1);
            GetScreen(stage[1].width);

            // food processing
            timestamp = 0;
            foodPosition = new List<Tuple<Point, Food>>();
            newFood = new Tuple<Point, Food>(new Point(7, 7), Food.standard);
            foodPosition.Insert(0, newFood);

            FPS = varijable.FPS;    // postavljanje osvježavanja

            snake = new Snake(varijable.width); // nova zmijica na (3,3) //TODO ovo treba ovisiti o vrsti levela
            DrawList(snake.getPosition(), Color.Green); // inicijalno crtanje zmije

            walls = stage[1].getWalls();
            DrawList(walls, Color.Purple);  // crta točke iz walls
        }

        public override void Draw()
        {
            // resetiranje pozadine
            ClearScreen();
            DrawList(walls, Color.Purple);  // crta točke iz walls

            updateGame();   // računi za frame

            // nacrtaj standardFood
            List<Point> standardFood = new List<Point>();
            foreach (Tuple<Point, Food> food in foodPosition)
            {
                if (food.Item2 == Food.standard) standardFood.Insert(0, food.Item1);
            }
            DrawList(standardFood, Color.Yellow);

            // nacrtaj poisonFood
            List<Point> poisonFood = new List<Point>();
            foreach (Tuple<Point, Food> food in foodPosition)
            {
                if (food.Item2 == Food.poison) poisonFood.Insert(0, food.Item1);
            }
            DrawList(poisonFood, Color.Red);

            // crtanje zmije
            DrawList(snake.getPosition(), Color.Green);

            // za svaki pomak gubi se bod
            if(direction.X != 0 || direction.Y != 0) varijable.score--;

            // crtanje ploče s bodovima
            labelLength.Text = $"Length: {snake.Length} / {varijable.goalLength}";
            labelScore.Text = $"Score: {varijable.score.ToString("00000000")}";
            labelStage.Text = $"Stage: {varijable.stage}";
            labelLevel.Text = $"Level: {varijable.level.ToString("0000")}";

            // provjera bodova
            if (varijable.score <= 0) varijable.snakeAlive = false;

            // provjera prelaska na novi stage
            if (varijable.goalLength == snake.Length) LevelUp();
            if (varijable.snakeAlive == false)
            {
                timer1.Stop();
                (new GameOverForm(varijable.score)).ShowDialog();
                Close();
            }
        }

        public override void KeyPressed()
        {
            switch (KeyCode)
            {
                case Keys.W:
                case Keys.Up:
                    if (direction.Y == 1 && direction.X == 0) break;
                    newDirection.Y = -1;
                    newDirection.X = 0;
                    break;

                case Keys.S:
                case Keys.Down:
                    if (direction.Y == -1 && direction.X == 0) break;
                    newDirection.Y = 1;
                    newDirection.X = 0;
                    break;

                case Keys.A:
                case Keys.Left:
                    if (direction.Y == 0 && direction.X == 1) break;
                    newDirection.Y = 0;
                    newDirection.X = -1;
                    break;

                case Keys.D:
                case Keys.Right:
                    if (direction.Y == 0 && direction.X == -1) break;
                    newDirection.Y = 0;
                    newDirection.X = 1;
                    break;
            }
        }

        // ------------------- POMOĆNE FUNKCIJE --------------------------------

        void LevelUp()
        {
            timestamp = 0;

            varijable.stage += 1;
            if (varijable.stage == 4)   // povećanje levela != povećanje stage-a
            {
                varijable.stage = 1;
                varijable.level += 1;

                // otežanje igre
                FPS = varijable.FPS + 2;    // poziv settera koji djeluje na timer forme
                varijable.poisonDamage += 1; 
            }
            // otežanje igre
            varijable.goalLength += 1;

            // za prelazak naa novi level dobiva se novi bodovni resurs
            varijable.score += varijable.level * varijable.goalLength * stage[varijable.stage].width * stage[varijable.stage].width;

            stage[varijable.stage] = new Stage(varijable.stage);
            GetScreen(stage[varijable.stage].width);

            snake = new Snake(varijable.width); // nova zmijica na (3,3) //TODO ovo treba ovisiti o vrsti levela
            DrawList(snake.getPosition(), Color.Green); // inicijalno crtanje zmije

            walls = stage[varijable.stage].getWalls();
            DrawList(walls, Color.Purple);  // crta točke iz walls

            direction = new Point(0, 0); // reset kontrola
            newDirection = new Point(0, 0);

            // food processing
            timestamp = 0;
            foodPosition = new List<Tuple<Point, Food>>();
            newFood = new Tuple<Point, Food>(new Point(7, 7), Food.standard);
            foodPosition.Insert(0, newFood);

            FPS = varijable.FPS;    // postavljanje osvježavanja
        }

        private bool isLegalFoodPosition(Point position, Point headPosition, List<Point> snakePosition)
        {
            bool isLegalPosition = true;
            //kolizija sa zmijom?
            foreach (Point snakeBody in snakePosition.ToList())
            {
                if (snakeBody == position)
                {
                    isLegalPosition = false;
                }
            }
            if (headPosition == position) isLegalPosition = false;

            //kolizija sa hranom?
            foreach (Tuple<Point, Food> food in foodPosition.ToList())
            {
                if (food.Item1 == position)
                {
                    isLegalPosition = false;
                    break;
                }
            }

            //kolizija sa zidom?
            foreach (Point wall in walls.ToList())
            {
                if (wall == position)
                {
                    isLegalPosition = false;
                }
            }

            if (isLegalPosition) return true;
            else return false;
        }

        private void newStandardFood(Point headPosition, List<Point> snakePosition)
        {
            //generiraj dok se ne nade dozvoljena pozicija
            while (true)
            {
                Point position = new Point(r.Next(0, varijable.width - 1), r.Next(0, varijable.width - 1));
                if (isLegalFoodPosition(position, headPosition, snakePosition))
                {
                    Tuple<Point, Food> newFood = new Tuple<Point, Food>(position, Food.standard);
                    foodPosition.Insert(0, newFood);
                    return;
                }
            }
        }
        private void newPoisonFood(Point headPosition, List<Point> snakePosition)
        {
            //generiraj dok se ne nade dozvoljena pozicija
            while (true)
            {
                Point position = new Point(r.Next(0, varijable.width - 1), r.Next(0, varijable.width - 1));
                if (isLegalFoodPosition(position, headPosition, snakePosition))
                {
                    Tuple<Point, Food> newFood = new Tuple<Point, Food>(position, Food.poison);
                    foodPosition.Insert(0, newFood);
                    return;
                }
            }
        }
        //nez jel ovo treba bit neki override
        private void updateGame()
        {
            //TODO mozda maknut event listenere dok se ova funkcija izvrsava
            direction = newDirection;

            if (direction.X == 0 && direction.Y == 0) return;

            timestamp++;
            //gdje ce se pomaknut u sljedecem otkucaju
            Point headPosition = snake.headPosition();
            headPosition.X += direction.X;
            headPosition.Y += direction.Y;

            //provjeri je li zmija u koliziji sama sa sobom
            bool isDead = false;
            List<Point> snakePosition = snake.getPosition();
            //moze umrijet da se zabi u sebe
            foreach (Point snakeBody in snakePosition.ToList())
            {
                if (snakeBody == headPosition)
                {
                    isDead = true;
                }
            }
            //moze umrjet da se zabi u zid
            foreach (Point wall in walls.ToList())
            {
                if (wall == headPosition)
                {
                    isDead = true;
                }
            }
            if (isDead == true)
            {
                direction.X = 0;
                direction.Y = 0;
                varijable.snakeAlive = false;
            }


            //provjeri je li zmija u koliziji s hranom
            bool hasEaten = false;
            foreach (Tuple<Point, Food> food in foodPosition.ToList())
            {
                if(food.Item1 == headPosition)
                {
                    snake.update(direction, food.Item2, varijable.poisonDamage);
                    foodPosition.Remove(food);
                    hasEaten = true;
                    if (food.Item2 == Food.standard)
                    {
                        newStandardFood(headPosition, snakePosition);
                    }
                }
            }

            if (!hasEaten)
            {
                snake.update(direction);    // kontrola zmije
            }

            //generiraj otrov(ne smije bit u koliziji sa zidom, sa zmijom ili s drugim hranama
            if (timestamp > 0 && varijable.snakeAlive && timestamp % (ulong)varijable.poisonInterval == 0)
            {
                newPoisonFood(headPosition, snakePosition);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zmijica
{
    #region Metavarijable
    /// <summary>
    /// Meta varijable. Svaka instanca Game ima svoju instancu.
    /// </summary>
    public class Varijable
    {
        // tehnički detalji
        public int width = 10;
        public bool snakeAlive = true;
        public bool snakeAIAlive = true;

        // težina igre
        public int goalLength = 8;
        public int poisonInterval = 20;
        public int randomInterval = 18;
        public int poisonDamage = 3;
        public int FPS = 4;

        //random foodthings
        public int randomPositiveDuration = 10;
        public int randomNegativeDuration = 6;


        // score screen
        public int score = 1000;
        public int stage = 1;
        public int level = 1;

        /// <summary>
        /// Specijalna inicijalizacija varijabli za versus mod igre.
        /// </summary>
        public void vsModeInit()
        {
            width = 30;
            FPS = 10;
            poisonDamage = 4;
            poisonInterval = 20;
            randomInterval = 5;
        }
    }

    #endregion

    internal class Game : GameForm
    {
        #region Konstruktor 

        /// <summary>
        /// Kako bismo osigurali funkcionalnost forme, na početku se 
        /// poziva <see cref="GameForm.GameForm"/>.
        /// </summary>
        public Game(ControlSettings sts) : base(sts) {}   // ovdje se ništa ne upisuje

        #endregion

        #region Varijable

        List<Point> walls = new List<Point>();    // sadrži koordinate zida
        Stage[] stage = new Stage[4];       // sadrži informacije o zidovima

        Snake snake;
        List<Tuple<Point, Food>> foodPosition;
        Point direction = new Point(0,0);    // testne kontrole //!mislim da ne treba(init smjer zmijice na 0,0)
        Point newDirection = new Point(0, 0);    // testne kontrole //!mislim da ne treba(init smjer zmijice na 0,0)
        Tuple<Point, Food> newFood;
        ulong timestamp;
        Random r = new Random();
        List<Point> transparentPoints;
        // ovo sam stavio tak da kad krene novi level moras dva puta stisnut neku komandu prije nego se zmija krene kretat
        // (stalno sam gubio jer iznenada krene u novi level)
        bool newLevel = false;

        // za random hranu
        public int randomTimeLeft = 0;
        public int randomMode = 0;

        // varijable za kontorlu zmije

        Keys tpEdgeActivator, tpSelfActivator;   // aktivatori za specijalne pokrete
        Keys up, down, left, right;  // generalne kontrole
        bool tpEdge, skipN, tpSelf;     // jesu li kretnje aktivirane posebnim tipkama
        int skipAmount;

        #endregion

        #region Testne varijable

        #endregion

        #region Setup, Draw, KeyPressed, KeyReleased

        public override void Setup() 
        {
            // kontrole se postavljaju ovisno o Options formi
            up = cSettings.up;
            down = cSettings.down;
            left = cSettings.left;
            right = cSettings.right;
            tpEdgeActivator = cSettings.tpEdgeActivator;
            tpSelfActivator = cSettings.tpSelfActovator;

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

            // napravio da je svaki level isti width pa moze bit ovdje
            transparentPoints = new List<Point>();
            transparentPoints.Insert(0, new Point(3, 3));
            transparentPoints.Insert(0, new Point(varijable.width - 4, 3));
            transparentPoints.Insert(0, new Point(3, varijable.width - 4));

            snake = new Snake(varijable.width); // nova zmijica na (3,3)
            //if(timestamp == 0) DrawList(snake.getPosition(), Color.Green); // inicijalno crtanje zmije

            walls = stage[1].getWalls();
            DrawList(walls, Color.Purple);  // crta točke iz walls

        }

        public override void Draw()
        {

            // resetiranje pozadine
            ClearScreen();
            DrawList(walls, Color.Purple);  // crta točke iz walls
            if (randomMode == 2) updateGameDouble();
            else updateGame();   // računi za frame

            transparentPoints.Insert(0, new Point(varijable.width - 4, varijable.width - 4));
            DrawList(transparentPoints, Color.DarkGray);

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

            // nacrtaj randomFood
            List<Point> randomFood = new List<Point>();
            foreach (Tuple<Point, Food> food in foodPosition)
            {
                if (food.Item2 == Food.random) randomFood.Insert(0, food.Item1);
            }
            DrawList(randomFood, Color.Orange);

            // crtanje zmije
            List<Point> snakePoints = snake.getPosition();
            if(randomMode == 1) DrawList(snakePoints, Color.LightCyan);
            else DrawList(snakePoints, Color.Green);

            List<Point> snakeInTransperentPoints = new List<Point>();
            foreach (Point snakePoint in snakePoints)
            {
                if (snakePoint == snake.headPosition()) continue;
                foreach (Point transparentPoint in transparentPoints)
                {
                    if (transparentPoint == snakePoint)
                    {
                        snakeInTransperentPoints.Insert(0, transparentPoint);
                    }
                }
            }
            DrawList(snakeInTransperentPoints, Color.LightCyan); // crtanje transparentnih polja na kojima se zmija nalazi

            List<Point> snakeHead = new List<Point>();
            snakeHead.Insert(0, snake.headPosition());
            DrawList(snakeHead, Color.DarkGreen);

            if (randomMode != 0) randomTimeLeft--;
            if (randomTimeLeft == 0)
            {
                randomMode = 0;
            }

            // za svaki pomak gubi se bod
            if (direction.X != 0 || direction.Y != 0) varijable.score--;

            // crtanje ploče s bodovima
            labelLength.Text = $"Length: {snake.Length} / {varijable.goalLength}";
            labelScore.Text = $"Score: {varijable.score.ToString("00000000")}";
            labelStage.Text = $"Stage: {varijable.stage}";
            labelLevel.Text = $"Level: {varijable.level.ToString("0000")}";

            // provjera bodova
            if (varijable.score <= 0) varijable.snakeAlive = false;

            // provjera prelaska na novi stage
            if (varijable.goalLength <= snake.Length) LevelUp();
            if (varijable.snakeAlive == false)
            {
                timer1.Stop();
                (new GameOverForm(varijable.score)).ShowDialog();
                Close();
            }

            // indikator korisniku da su uključeni aktivatori za specijalne kretnje
            labelSkipAmount.Text = skipAmount.ToString();
            labelSkipAmount.ForeColor = skipN ? Color.Yellow : Color.Black;
            if (tpSelf) pictureBox.BackColor = Color.Green;
            else if (tpEdge) pictureBox.BackColor = Color.Purple;
            else pictureBox.BackColor = Color.Black;
        }

        public override void KeyPressed()
        {

            if(KeyCode == up)
            {
                if (newLevel)
                {
                    newLevel = false;
                    return;
                }
                if (ActivateTp(new Point(0,-1))) return;
                // default ponašanje : skretanje
                else if (direction.Y == 1 && direction.X == 0) return;
                newDirection.Y = -1;
                newDirection.X = 0;
            } 
            else if(KeyCode == down)
            {
                if (newLevel)
                {
                    newLevel = false;
                    return;
                }
                if (ActivateTp(new Point(0,1))) return;
                // default ponašanje : skretanje
                else if (direction.Y == -1 && direction.X == 0) return;
                newDirection.Y = 1;
                newDirection.X = 0;
            } 
            else if(KeyCode == left)
            {
                if (newLevel)
                {
                    newLevel = false;
                    return;
                }
                if (ActivateTp(new Point(-1,0))) return;
                // default ponašanje : skretanje
                else if (direction.Y == 0 && direction.X == 1) return;
                newDirection.Y = 0;
                newDirection.X = -1;
            } 
            else if(KeyCode == right)
            {
                if (newLevel)
                {
                    newLevel = false;
                    return;
                }
                if (ActivateTp(new Point(1,0))) return;
                // default ponašanje : skretanje
                else if (direction.Y == 0 && direction.X == -1) return;
                newDirection.Y = 0;
                newDirection.X = 1;
            }

            // posebne kretnje

            else if(KeyCode == tpEdgeActivator || ModifierKeys == tpEdgeActivator)
            {
                skipN = false; tpSelf = false;  // ostale deaktiviramo
                tpEdge = true;
            }
            else if (KeyCode == tpSelfActivator || ModifierKeys == tpSelfActivator)
            {
                skipN = false; tpEdge = false; // ostale deaktiviramo
                tpSelf = true;
                pictureBox.BackColor = Color.Green;
            }
            else if (KeyCode >= Keys.NumPad1 && KeyCode <= Keys.NumPad9)
            {
                skipAmount = (int)KeyCode - (int)Keys.NumPad0;
                tpSelf = false; tpEdge = false; // ostale deaktiviramo
                skipN = true;
            }
        }

        public override void KeyReleased()
        {
            //if (ModifierKeys != Keys.Shift) tpSelf = false;

            if (KeyCode == tpEdgeActivator)
            {
                tpEdge = false;
            }
            else if (KeyCode == tpSelfActivator)
            {
                tpSelf = false;
            }
            else if (KeyCode >= Keys.NumPad1 && KeyCode <= Keys.NumPad9)
            // otpušten je broj na tipkovnici
            {
                skipN = false;
            }
            if(ModifierKeys != tpSelfActivator && ModifierKeys != tpEdgeActivator)
            {
                tpSelf = tpEdge = false;
            }
        }

        #endregion

        #region Pomoćne funkcije

        /// <summary>
        /// Provjerava hoće li zmijica koristiti posebne kontrole
        /// </summary>
        /// <param name="dir"></param>
        /// <returns>bool</returns>
        bool ActivateTp(Point dir)
        {
            if(tpSelf)
            {
                TeleportToSelf(dir);
                return true;
            }
            else if(tpEdge)
            {
                TeleportToEdge(dir);
                return true;
            }
            else if(skipN)
            {
                SkipN(dir, skipAmount);
                return true;
            }

            return false;   // daje znak da je default ponašanje u pitanju
        }
        /// <summary>
        /// Zmija ide do ruba u smjeru direction.
        /// Direction iz skupa {(-1,0),(1,0),(0,1),(0,-1)}
        /// </summary>
        /// <param name="direction"></param>
        void TeleportToEdge(Point direction)
        {
            if (OppositeDirection(direction, this.direction)) return;

            //? ovo kao prima direction znaci ako je isao u nekom smjeru, moze se pomakmnut do zida da zadrzi smjer kretanja? ok

            while (true)
            {
                //gdje ce se pomaknut u sljedecem otkucaju
                Point headPosition = snake.headPosition();
                headPosition.X += direction.X;
                headPosition.Y += direction.Y;
                List<Point> snakePosition = snake.getPosition();
                if (headPosition.X == -1 || headPosition.Y == -1 || headPosition.X == varijable.width || headPosition.Y == varijable.width)
                    return;

                    foreach (Point wall in walls)
                {
                    if (headPosition == wall)
                        return;
                }

                if (snakeDying(headPosition, snakePosition))
                {
                    direction.X = 0;
                    direction.Y = 0;
                    varijable.snakeAlive = false;
                    timestamp = 0;
                }

                //provjeri je li zmija u koliziji s hranom
                bool hasEaten = false;
                foreach (Tuple<Point, Food> food in foodPosition.ToList())
                {
                    if (food.Item1 == headPosition)
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

                //generiraj otrov(ne smije bit u koliziji sa zidom, sa zmijom ili s drugim hranama)
                if (timestamp > 0 && varijable.snakeAlive && timestamp % (ulong)varijable.poisonInterval == 0)
                {
                    newPoisonFood(headPosition, snakePosition);
                }
            }
        }
        /// <summary>
        /// ispituje jesu li zadani smjerovi suprotni
        /// Direction iz skupa {(-1,0),(1,0),(0,1),(0,-1)}
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>bool</returns>
        bool OppositeDirection(Point a, Point b)
        {
            if (a.X + b.X == 0 && a.Y + b.Y == 0) return true;
            return false;
        }
        /// <summary>
        /// Zmija preskače n polja u smjeru direction.
        /// Direction iz skupa {(-1,0),(1,0),(0,1),(0,-1)}
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="n"></param>
        void SkipN(Point direction, int n)
        {
            if (OppositeDirection(direction, this.direction)) return;

            for(int i = 0; i < n; i++)
            {
                //gdje ce se pomaknut u sljedecem otkucaju
                Point headPosition = snake.headPosition();
                headPosition.X += direction.X;
                headPosition.Y += direction.Y;
                List<Point> snakePosition = snake.getPosition();
                if (headPosition.X == -1 || headPosition.Y == -1 || headPosition.X == varijable.width || headPosition.Y == varijable.width)
                    return;

                if (snakeDying(headPosition, snakePosition))
                {
                    direction.X = 0;
                    direction.Y = 0;
                    varijable.snakeAlive = false;
                    timestamp = 0;
                }

                //provjeri je li zmija u koliziji s hranom
                bool hasEaten = false;
                foreach (Tuple<Point, Food> food in foodPosition.ToList())
                {
                    if (food.Item1 == headPosition)
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

                //generiraj otrov(ne smije bit u koliziji sa zidom, sa zmijom ili s drugim hranama)
                if (timestamp > 0 && varijable.snakeAlive && timestamp % (ulong)varijable.poisonInterval == 0)
                {
                    newPoisonFood(headPosition, snakePosition);
                }
            }
        }
        /// <summary>
        /// Zmija se teleportira do sebe u smjeru direction.
        /// Što ako u tom smjeru nema zmije? Ništa se ne događa.
        /// Direction iz skupa {(-1,0),(1,0),(0,1),(0,-1)}
        /// </summary>
        /// <param name="direction"></param>
        void TeleportToSelf(Point direction)
        {
            if (OppositeDirection(direction, this.direction)) return;
            Point targetLocation = new Point();
            bool isAplicable = false;
            Point currentHeadPosition = snake.headPosition();
            List<Point> currentSnakePosition = snake.getPosition();
            while (currentHeadPosition.X < varijable.width && currentHeadPosition.Y < varijable.width && currentHeadPosition.X > -1 && currentHeadPosition.Y > -1)
            {
                currentHeadPosition.X += direction.X;
                currentHeadPosition.Y += direction.Y;
                foreach(Point snake in currentSnakePosition)
                {
                    if(snake == currentHeadPosition)
                    {
                        isAplicable = true;
                        targetLocation = currentHeadPosition;
                        break;
                    }
                    if(isAplicable == true) break;
                }
            }
            if(isAplicable == false) return;

            //? ovo kao prima direction znaci ako je isao u nekom smjeru, moze se pomakmnut do zida da zadrzi smjer kretanja? ok

            while (true)
            {
                //gdje ce se pomaknut u sljedecem otkucaju
                Point headPosition = snake.headPosition();
                headPosition.X += direction.X;
                headPosition.Y += direction.Y;
                if (headPosition == targetLocation) break;

                List<Point> snakePosition = snake.getPosition();
                if (headPosition.X == -1 || headPosition.Y == -1 || headPosition.X == varijable.width || headPosition.Y == varijable.width)
                    return;

                foreach (Point wall in walls)
                {
                    if (headPosition == wall)
                        return;
                }

                foreach (Point snake in snakePosition)
                {
                    if (headPosition == snake)
                        return;
                }

                if (snakeDying(headPosition, snakePosition))
                {
                    direction.X = 0;
                    direction.Y = 0;
                    varijable.snakeAlive = false;
                    timestamp = 0;
                }

                //provjeri je li zmija u koliziji s hranom
                bool hasEaten = false;
                foreach (Tuple<Point, Food> food in foodPosition.ToList())
                {
                    if (food.Item1 == headPosition)
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

                //generiraj otrov(ne smije bit u koliziji sa zidom, sa zmijom ili s drugim hranama)
                if (timestamp > 0 && varijable.snakeAlive && timestamp % (ulong)varijable.poisonInterval == 0)
                {
                    newPoisonFood(headPosition, snakePosition);
                }
            }
        }
        /// <summary>
        /// Prebacuje na sljedecu razinu igre
        /// </summary>
        void LevelUp()
        {
            newLevel = true;
            timestamp = 0;

            varijable.stage += 1;

            if (varijable.stage == 4)   // povećanje levela != povećanje stage-a
            {
                varijable.stage = 1;
                varijable.level += 1;

                // otežanje igre
                varijable.poisonDamage += 1;
                varijable.goalLength += 2;
                FPS = varijable.FPS + 2;
            }

            //nakon sto se ovdje inicijalizira treba uvijek korist varijable.width, ovak je neuredno
            varijable.width = stage[varijable.stage].width;

            // za prelazak naa novi level dobiva se novi bodovni resurs
            varijable.score += varijable.level * varijable.goalLength * stage[varijable.stage].width * stage[varijable.stage].width;

            //stage[varijable.stage] = new Stage(varijable.stage);
            //GetScreen(stage[varijable.stage].width);

            snake = new Snake(varijable.width); // nova zmijica na (3,3)
            //DrawList(snake.getPosition(), Color.Green); // inicijalno crtanje zmije

            walls = stage[varijable.stage].getWalls();
            DrawList(walls, Color.Purple);  // crta točke iz walls

            direction = new Point(0, 0); // reset kontrola
            newDirection = new Point(0, 0);

            // food processing
            //TODO jedan timestamp = 0 se moze izbrisat?
            timestamp = 0;
            foodPosition = new List<Tuple<Point, Food>>();
            newFood = new Tuple<Point, Food>(new Point(7, 7), Food.standard);
            foodPosition.Insert(0, newFood);

            FPS = varijable.FPS;    // postavljanje osvježavanja
        }
        /// <summary>
        /// Ispituje je li pozicija slobodna za generiranje nove hrane
        /// </summary>
        /// <param name="position"></param>
        /// <param name="headPosition"></param>
        /// <param name="snakePosition"></param>
        /// <returns>bool</returns>
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
        /// <summary>
        /// Generira novu hranu tipa standard
        /// </summary>
        /// <param name="headPosition"></param>
        /// <param name="snakePosition"></param>
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
        /// <summary>
        /// Generira novu hranu tipa poison
        /// </summary>
        /// <param name="headPosition"></param>
        /// <param name="snakePosition"></param>
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
        /// <summary>
        /// Generira novu hranu tipa random
        /// </summary>
        /// <param name="headPosition"></param>
        /// <param name="snakePosition"></param>
        private void newRandomFood(Point headPosition, List<Point> snakePosition)
        {
            //generiraj dok se ne nade dozvoljena pozicija
            while (true)
            {
                Point position = new Point(r.Next(0, varijable.width - 1), r.Next(0, varijable.width - 1));
                if (isLegalFoodPosition(position, headPosition, snakePosition))
                {
                    Tuple<Point, Food> newFood = new Tuple<Point, Food>(position, Food.random);
                    foodPosition.Insert(0, newFood);
                    return;
                }
            }
        }
        /// <summary>
        /// Ispituje hoce li zmija umrijeti na zadanoj poziciji
        /// </summary>
        /// <param name="headPosition"></param>
        /// <param name="snakePosition"></param>
        /// <returns>bool</returns>
        private bool snakeDying(Point headPosition, List<Point> snakePosition)
        {
            //provjeri je li zmija u koliziji sama sa sobom
            bool isDead = false;
            //moze umrijet da se zabi u sebe
            //dodajem if koj ce napravit da na svakom levelu bude nekoliko polja za koja nije frka ak se zmij zabije sama u sebe
            bool canPassThrough = false;
            if(randomMode == 1) canPassThrough = true;
            foreach (Point transparentPoint in transparentPoints)
            {
                if (transparentPoint == headPosition)
                {
                    canPassThrough = true;
                    break;
                }
            }
            if (!canPassThrough)
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
            return isDead;
        }
        /// <summary>
        /// Azurira igru za jedan vremenski otkucaj
        /// </summary>
        private void updateGame()
        {
            direction = newDirection;
            if (direction.X == 0 && direction.Y == 0) return;

            timestamp++;
            //gdje ce se pomaknut u sljedecem otkucaju
            Point headPosition = snake.headPosition();
            headPosition.X += direction.X;
            headPosition.Y += direction.Y;
            List<Point> snakePosition = snake.getPosition();


            if (snakeDying(headPosition, snakePosition))
            {
                direction.X = 0;
                direction.Y = 0;
                varijable.snakeAlive = false;
                timestamp = 0;
            }
            //provjeri je li zmija u koliziji s hranom
            bool hasEaten = false;
            foreach (Tuple<Point, Food> food in foodPosition.ToList())
            {
                if(food.Item1 == headPosition)
                {
                    int resultOfEat = snake.update(direction, food.Item2, varijable.poisonDamage);
                    //zmija moze prolazit kroz sebe
                    if(resultOfEat == 1)
                    {
                        randomMode = 1;
                        randomTimeLeft = varijable.randomPositiveDuration;

                    }
                    //zmija se krece za dva
                    else if(resultOfEat == 2)
                    {
                        randomMode = 2;
                        randomTimeLeft = varijable.randomNegativeDuration;
                    }
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

            //generiraj otrov(ne smije bit u koliziji sa zidom, sa zmijom ili s drugim hranama)
            if (timestamp > 0 && varijable.snakeAlive && timestamp % (ulong)varijable.poisonInterval == 0)
            {
                newPoisonFood(headPosition, snakePosition);
            }
            //generiraj otrov(ne smije bit u koliziji sa zidom, sa zmijom ili s drugim hranama)
            if (timestamp > 0 && varijable.snakeAlive && timestamp % (ulong)varijable.randomInterval == 0)
            {
                newRandomFood(headPosition, snakePosition);
            }
        }
        /// <summary>
        /// Azurira igru za jedan vremenski otkucaj u slucaju kada se zmija krece ubrzano
        /// </summary>
        private void updateGameDouble()
        {
            direction = newDirection;
            if (direction.X == 0 && direction.Y == 0) return;

            timestamp++;
            //gdje ce se pomaknut u sljedecem otkucaju
            Point headPosition = snake.headPosition();
            headPosition.X += direction.X;
            headPosition.Y += direction.Y;
            List<Point> snakePosition = snake.getPosition();


            if (snakeDying(headPosition, snakePosition))
            {
                direction.X = 0;
                direction.Y = 0;
                varijable.snakeAlive = false;
                timestamp = 0;
            }

            //provjeri je li zmija u koliziji s hranom
            bool hasEaten = false;
            foreach (Tuple<Point, Food> food in foodPosition.ToList())
            {
                if (food.Item1 == headPosition)
                {
                    int resultOfEat = snake.update(direction, food.Item2, varijable.poisonDamage);
                    //zmija moze prolazit kroz sebe
                    if (resultOfEat == 1)
                    {
                        randomMode = 1;
                        randomTimeLeft = varijable.randomPositiveDuration;

                    }
                    //zmija se krece za dva
                    else if (resultOfEat == 2)
                    {
                        randomMode = 2;
                        randomTimeLeft = varijable.randomNegativeDuration;
                    }
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


            //jos jednom jer smo u tom modeu
            //gdje ce se pomaknut u sljedecem otkucaju
            headPosition = snake.headPosition();
            headPosition.X += direction.X;
            headPosition.Y += direction.Y;
            snakePosition = snake.getPosition();


            if (snakeDying(headPosition, snakePosition))
            {
                direction.X = 0;
                direction.Y = 0;
                varijable.snakeAlive = false;
                timestamp = 0;
            }

            //provjeri je li zmija u koliziji s hranom
            hasEaten = false;
            foreach (Tuple<Point, Food> food in foodPosition.ToList())
            {
                if (food.Item1 == headPosition)
                {
                    int resultOfEat = snake.update(direction, food.Item2, varijable.poisonDamage);
                    //zmija moze prolazit kroz sebe
                    if (resultOfEat == 1)
                    {
                        randomMode = 1;
                        randomTimeLeft = varijable.randomPositiveDuration;

                    }
                    //zmija se krece za dva
                    else if (resultOfEat == 2)
                    {
                        randomMode = 2;
                        randomTimeLeft = varijable.randomNegativeDuration;
                    }
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

            //generiraj otrov(ne smije bit u koliziji sa zidom, sa zmijom ili s drugim hranama)
            if (timestamp > 0 && varijable.snakeAlive && timestamp % (ulong)varijable.poisonInterval == 0)
            {
                newPoisonFood(headPosition, snakePosition);
            }
            //generiraj otrov(ne smije bit u koliziji sa zidom, sa zmijom ili s drugim hranama)
            if (timestamp > 0 && varijable.snakeAlive && timestamp % (ulong)varijable.randomInterval == 0)
            {
                newRandomFood(headPosition, snakePosition);
            }
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zmijica
{
    internal class GameVersus : GameForm
    {
        #region Konstruktor 

        /// <summary>
        /// Kako bismo osigurali funkcionalnost forme, na početku se 
        /// poziva <see cref="GameForm.GameForm"/>.
        /// </summary>
        public GameVersus(ControlSettings sts) : base(sts) { }   // ovdje se ništa ne upisuje

        #endregion

        #region Varijable

        Stage[] stage = new Stage[4];       // sadrži informacije o zidovima

        Snake snake;
        SnakeAI snakeAI;
        List<Tuple<Point, Food>> foodPosition;
        Point direction = new Point(0, 0);
        Point newDirection = new Point(0, 0);
        Tuple<Point, Food> newFood;
        ulong timestamp;
        Random r = new Random();
        List<Point> transparentPoints;

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
            // default kontrole
            up = Keys.W;
            down = Keys.S;
            left = Keys.A;
            right = Keys.D;
            tpEdgeActivator = Keys.Shift;
            tpSelfActivator = Keys.Space;

            #region mala modifikacija prozora

            varijable.vsModeInit();     // tamo su vrijednosti za ovaj mode
            GetScreen(varijable.width);
            Width -= 15;
            activeScreen.Height -= 15;
            BackColor = Color.FromArgb(50,0,50);
            labelLevel.Hide();
            labelStage.Hide();

            #endregion

            // food processing
            timestamp = 0;
            foodPosition = new List<Tuple<Point, Food>>();
            //TODO nesto zanimljivije od 7,7
            newFood = new Tuple<Point, Food>(new Point(7, 7), Food.standard);
            foodPosition.Insert(0, newFood);

            FPS = varijable.FPS;    // postavljanje osvježavanja

            // napravio da je svaki level isti width pa moze bit ovdje
            transparentPoints = new List<Point>();
            for(int i = 2; i < varijable.width; i += 4)
            {
                for(int j = 2; j < varijable.width; j += 4)
                {
                    transparentPoints.Insert(0, new Point(i, j));
                }
            }


            snake = new Snake(varijable.width);
            snakeAI = new SnakeAI(varijable.width);
        }

        public override void Draw()
        {

            // resetiranje pozadine
            ClearScreen();
            if(timestamp % 2 == 0) updateGame();   // računi za frame
            else updateGameAI();   // računi za frame

            DrawList(transparentPoints, Color.DarkGray);

            // nacrtaj standardFood
            List<Point> standardFood = new List<Point>();
            foreach (Tuple<Point, Food> food in foodPosition)
            {
                if (food.Item2 == Food.standard) standardFood.Insert(0, food.Item1);
            }
            DrawList(standardFood, Color.Yellow);

            // crtanje zmije
            List<Point> snakePoints = snake.getPosition();
            DrawList(snakePoints, Color.Green);

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


            // crtanje AI zmije
            List<Point> snakeAIPoints = snakeAI.getPosition();
            DrawList(snakeAIPoints, Color.Blue);

            List<Point> snakeAIInTransperentPoints = new List<Point>();
            foreach (Point snakeAIPoint in snakeAIPoints)
            {
                if (snakeAIPoint == snakeAI.headPosition()) continue;
                foreach (Point transparentPoint in transparentPoints)
                {
                    if (transparentPoint == snakeAIPoint)
                    {
                        snakeAIInTransperentPoints.Insert(0, transparentPoint);
                    }
                }
            }
            DrawList(snakeAIInTransperentPoints, Color.LightCyan); // crtanje transparentnih polja na kojima se zmija nalazi

            List<Point> snakeAIHead = new List<Point>();
            snakeAIHead.Insert(0, snakeAI.headPosition());
            DrawList(snakeAIHead, Color.DarkBlue);


            // za svaki pomak gubi se bod
            //TODO ak je stisnut shift(il kaj got onda gubi bod samo svaki drugi pomak)
            if (direction.X != 0 || direction.Y != 0) varijable.score--;

            // crtanje ploče s bodovima
            labelLength.Text = $"Length: {snake.Length}";
            labelScore.Text = $"Score: {varijable.score.ToString("00000000")}";

            // provjera bodova
            if (varijable.score <= 0) varijable.snakeAlive = false;

            // provjera kraja
            if (varijable.snakeAlive == false)
            {
                timer1.Stop();
                (new GameOverForm(varijable.score)).ShowDialog();
                Close();
            }

            // indikator korisniku da su uključeni aktivatori za specijalne kretnje
            labelSkipAmount.Text = skipAmount.ToString();
            labelSkipAmount.ForeColor = skipN ? Color.Yellow : BackColor;
            if (tpSelf) pictureBox.BackColor = Color.Green;
            else if (tpEdge) pictureBox.BackColor = Color.Purple;
            else pictureBox.BackColor = BackColor;
        }

        public override void KeyPressed()
        {

            if (KeyCode == up)
            {

                // default ponašanje : skretanje
                if (direction.Y == 1 && direction.X == 0) return;
                newDirection.Y = -1;
                newDirection.X = 0;
            }
            else if (KeyCode == down)
            {
                // default ponašanje : skretanje
                if (direction.Y == -1 && direction.X == 0) return;
                newDirection.Y = 1;
                newDirection.X = 0;
            }
            else if (KeyCode == left)
            {
                // default ponašanje : skretanje
                if (direction.Y == 0 && direction.X == 1) return;
                newDirection.Y = 0;
                newDirection.X = -1;
            }
            else if (KeyCode == right)
            {
                // default ponašanje : skretanje
                if (direction.Y == 0 && direction.X == -1) return;
                newDirection.Y = 0;
                newDirection.X = 1;
            }

            // posebne kretnje

            else if (KeyCode == tpEdgeActivator || ModifierKeys == tpEdgeActivator)
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

            /*switch (KeyCode)
            {
                //case Keys.W:
                //case Keys.Up:
                case up:
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

                //case Keys.A:
                //case Keys.Left:
                case "left":
                    if (direction.Y == 0 && direction.X == 1) break;
                    newDirection.Y = 0;
                    newDirection.X = -1;
                    break;

                //case Keys.D:
                //case Keys.Right:
                case "right":
                    if (direction.Y == 0 && direction.X == -1) break;
                    newDirection.Y = 0;
                    newDirection.X = 1;
                    break;

                case "teleport":
                    Teleport(varijable.tpDirection);
                    break;

                case ""

                default:
                    break;
            }*/
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
            if (ModifierKeys != tpSelfActivator && ModifierKeys != tpEdgeActivator)
            {
                tpSelf = tpEdge = false;
            }
        }

        #endregion

        #region Pomoćne funkcije

        private bool isLegalFoodPosition(Point position, Point snakeHeadPosition, List<Point> snakePosition, Point snakeAIHeadPosition, List<Point> snakeAIPosition)
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
            if (snakeHeadPosition == position) isLegalPosition = false;
            //kolizija sa zmijomAI?
            foreach (Point snakeBody in snakeAIPosition.ToList())
            {
                if (snakeBody == position)
                {
                    isLegalPosition = false;
                }
            }
            if (snakeAIHeadPosition == position) isLegalPosition = false;

            if (isLegalPosition) return true;
            else return false;
        }
        private void newStandardFood(Point snakeHeadPosition, List<Point> snakePosition, Point snakeAIHeadPosition, List<Point> snakeAIPosition)
        {
            //generiraj dok se ne nade dozvoljena pozicija
            while (true)
            {
                //TODO ne skroz random?
                Point position = new Point(r.Next(0, varijable.width - 1), r.Next(0, varijable.width - 1));
                if (isLegalFoodPosition(position, snakeHeadPosition, snakePosition, snakeAIHeadPosition, snakeAIPosition))
                {
                    Tuple<Point, Food> newFood = new Tuple<Point, Food>(position, Food.standard);
                    foodPosition.Insert(0, newFood);
                    return;
                }
            }
        }
        private bool snakeDying(Point snakeHeadPosition, List<Point> snakePosition, List<Point> snakeAIPosition)
        {
            //provjeri je li zmija u koliziji sama sa sobom
            bool isDead = false;
            //moze umrijet da se zabi u sebe
            //dodajem if koj ce napravit da na svakom levelu bude nekoliko polja za koja nije frka ak se zmij zabije sama u sebe
            bool canPassThrough = false;
            foreach (Point transparentPoint in transparentPoints)
            {
                if (transparentPoint == snakeHeadPosition)
                {
                    canPassThrough = true;
                    break;
                }
            }
            if (!canPassThrough)
            {
                //zabila se u sebe
                foreach (Point snakeBody in snakePosition.ToList())
                {
                    if (snakeBody == snakeHeadPosition)
                    {
                        isDead = true;
                    }
                }
                //zabila se u snakeAI
                foreach (Point snakeAIBody in snakeAIPosition.ToList())
                {
                    if (snakeAIBody == snakeHeadPosition)
                    {
                        isDead = true;
                    }
                }
            }
            return isDead;
        }
        private bool snakeAIDying(Point snakeAIHeadPosition, List<Point> snakeAIPosition, List<Point> snakePosition)
        {
            //provjeri je li zmija u koliziji sama sa sobom
            bool isDead = false;
            //moze umrijet da se zabi u sebe
            //dodajem if koj ce napravit da na svakom levelu bude nekoliko polja za koja nije frka ak se zmij zabije sama u sebe
            bool canPassThrough = false;
            foreach (Point transparentPoint in transparentPoints)
            {
                if (transparentPoint == snakeAIHeadPosition)
                {
                    canPassThrough = true;
                    break;
                }
            }
            if (!canPassThrough)
            {
                //zabila se u sebe
                foreach (Point snakeSIBody in snakeAIPosition.ToList())
                {
                    if (snakeSIBody == snakeAIHeadPosition)
                    {
                        isDead = true;
                    }
                }
                //zabila se u snakeAI
                foreach (Point snakeBody in snakePosition.ToList())
                {
                    if (snakeBody == snakeAIHeadPosition)
                    {
                        isDead = true;
                    }
                }
            }
            return isDead;
        }
        private void updateGame()
        {
            //TODO mozda maknut event listenere dok se ova funkcija izvrsava
            direction = newDirection;

            if (direction.X == 0 && direction.Y == 0) return;

            timestamp++;
            //gdje ce se pomaknut u sljedecem otkucaju
            Point snakeHeadPosition = snake.headPosition();
            snakeHeadPosition.X += direction.X;
            snakeHeadPosition.Y += direction.Y;
            List<Point> snakePosition = snake.getPosition();
            List<Point> snakeAIPosition = snakeAI.getPosition();
            Point snakeAIHeadPosition = snakeAI.headPosition();


            if (snakeDying(snakeHeadPosition, snakePosition, snakeAIPosition))
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
                if (food.Item1 == snakeHeadPosition)
                {
                    snake.update(direction, food.Item2);
                    foodPosition.Remove(food);
                    hasEaten = true;
                    newStandardFood(snakeHeadPosition, snakePosition, snakeAIHeadPosition, snakeAIPosition);
                }
            }

            if (!hasEaten)
            {
                snake.update(direction);    // kontrola zmije
            }
        }

        private void updateGameAI()
        {
            timestamp++;
            //gdje ce se pomaknut u sljedecem otkucaju
            Point snakeAIHeadPosition = snakeAI.headPosition();
            List<Point> snakeAIPosition = snakeAI.getPosition();
            List<Point> snakePosition = snake.getPosition();
            Point snakeHeadPosition = snake.headPosition();
            Point directionAI = snakeAI.newDirection(foodPosition.First().Item1, snakePosition, transparentPoints);
            snakeAIHeadPosition.X += directionAI.X;
            snakeAIHeadPosition.Y += directionAI.Y;

            if (snakeAIDying(snakeAIHeadPosition, snakeAIPosition, snakePosition))
            {
                varijable.snakeAIAlive = false;
                timestamp = 0;
            }

            //provjeri je li zmija u koliziji s hranom
            bool hasEaten = false;
            foreach (Tuple<Point, Food> food in foodPosition.ToList())
            {
                if (food.Item1 == snakeAIHeadPosition)
                {
                    snakeAI.update(foodPosition.First().Item1, snakeAIPosition, transparentPoints, food.Item2);
                    foodPosition.Remove(food);
                    hasEaten = true;
                    newStandardFood(snakeHeadPosition, snakePosition, snakeAIHeadPosition, snakeAIPosition);
                }
            }

            if (!hasEaten)
            {
                snakeAI.update(foodPosition.First().Item1, snakePosition, transparentPoints);    // kontrola zmije
            }
        }

        #endregion
    }
}

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

        Keys up, down, left, right;  // generalne kontrole

        #endregion

        #region Testne varijable

        #endregion

        #region Setup, Draw, KeyPressed, KeyReleased

        public override void Setup()
        {
            // kontrole iz postavki
            up = cSettings.up;
            down = cSettings.down;
            left = cSettings.left;
            right = cSettings.right;

            #region mala modifikacija prozora za versus mode

            varijable.vsModeInit();     // tamo su vrijednosti za ovaj mode
            GetScreen(varijable.width);
            Width -= 15;
            activeScreen.Height -= 15;
            BackColor = Color.FromArgb(50,0,50);
            labelLevel.Hide();
            labelStage.Hide();
            labelSkipAmount.Hide();

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

            // crtanje ploče s bodovima
            labelLength.Text = $"Length: {snake.Length}";
            labelScore.Text = $"Score: {varijable.score.ToString("00000000")}";

            // provjera kraja
            if (varijable.snakeAlive == false)
            {
                timer1.Stop();
                (new GameOverForm(varijable.score)).ShowDialog();
                Close();
            }
        }
        public override void KeyPressed()
        {
            if(KeyCode == Keys.P)
            {
                timer1.Stop();
                varijable.paused = true;
                HelpScreen hc = new HelpScreen(cSettings);
                hc.ShowDialog();
                varijable.paused = false;
                timer1.Start();
            }
            else if (KeyCode == up)
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
        }

        // Ne koristi se ovdje.
        public override void KeyReleased() { }

        #endregion

        #region Pomoćne funkcije
        /// <summary>
        /// Ispituje je li pozicija slobodna za generiranje nove hrane
        /// </summary>
        /// <param name="position"></param>
        /// <param name="snakeHeadPosition"></param>
        /// <param name="snakePosition"></param>
        /// <param name="snakeAIHeadPosition"></param>
        /// <param name="snakeAIPosition"></param>
        /// <returns>bool</returns>
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
        /// <summary>
        /// Generira novu hranu tipa standard
        /// </summary>
        /// <param name="snakeHeadPosition"></param>
        /// <param name="snakePosition"></param>
        /// <param name="snakeAIHeadPosition"></param>
        /// <param name="snakeAIPosition"></param>
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
        /// <summary>
        /// Ispituje hoce li zmija umrijeti na zadanoj poziciji
        /// </summary>
        /// <param name="snakeHeadPosition"></param>
        /// <param name="snakePosition"></param>
        /// <param name="snakeAIPosition"></param>
        /// <returns>bool</returns>
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
        /// <summary>
        /// Ispituje hoce li zmija koju upravlja racunalo umrijeti na zadanoj poziciji
        /// </summary>
        /// <param name="snakeAIHeadPosition"></param>
        /// <param name="snakeAIPosition"></param>
        /// <param name="snakePosition"></param>
        /// <returns>bool</returns>
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
        /// <summary>
        /// Azurira igru za jedan vremenski otkucaj kada je igraceva zmija na potezu
        /// </summary>
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
                    // dodajem score increase
                    varijable.score += varijable.reward * snake.Length;
                    hasEaten = true;
                    newStandardFood(snakeHeadPosition, snakePosition, snakeAIHeadPosition, snakeAIPosition);
                }
            }

            if (!hasEaten)
            {
                snake.update(direction);    // kontrola zmije
            }
        }
        /// <summary>
        /// Azurira igru za jedan vremenski otkucaj kada je zmija kojom upravlja racunalo na potezu
        /// </summary>
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

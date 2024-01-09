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
        public Game() : base() { }

        /*
        U testnom primjeru zmija kreće s (1,1) i kreće se u desno. Ako izađe izvan ekrana se program ruši zbog
        exceptiona. Postavljanjem FPS se mijenja brzina. Strelicama na tipkovnici se kontrolira smjer kretanja.
        */

        // test data
        SnakeProto snake;   // testna zmijica
        Point direction = new Point(1,0);    // testne kontrole
        List<Point> pts = new List<Point>();    // sadrži koordinate zida

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
            FPS = 5;    // postavljanje osvježavanja

            snake = new SnakeProto(this, 1, 1); // nova zmijica na (5,5)
            DrawList(snake.pos(), Color.Green); // inicijalno crtanje zmije

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

        public override void Draw() 
        {
            // resetiranje pozadine
            ClearScreen();
            DrawList(pts, Color.Purple);  // crta točke iz pts

            // crtanje zmije
            snake.update(direction);    // kontrola zmije
            DrawList(snake.pos(), Color.Green);
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

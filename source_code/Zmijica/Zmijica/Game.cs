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

        // test data
        SnakeProto snake;   // testna zmijica
        List<Point> pts = new List<Point>();

        string[] wallString = new string[] {
            "##########",
            "#........#",
            "#........#",
            "#........#",
            "#...#....#",
            "#...#....#",
            "#........#",
            "#........#",
            "#........#",
            "##########"
        };

        public override void Setup() 
        {
            InitializeScreen(10);   // za rezoluciju ekrana
            FPS = 1;    // postavljanje osvježavanja

            snake = new SnakeProto(this, 5, 5); // nova zmijica na (5,5)

            // crtanje testnog zida
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
            // crtanje zmije
            snake.update();
            DrawList(snake.draw(), Color.Green);
        }

        public override void KeyPressed() 
        {

        }
    }
}

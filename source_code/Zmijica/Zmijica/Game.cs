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
        public Game() : base() { }

        public override void Setup() // scene and variable setup
        {
            
        }

        public override void Draw() // frame preprocessing
        {
            // test code
            graphics.Clear(Color.Black);
            graphics.FillRectangle(snakeBrush, MousePosition.X, MousePosition.Y, 50, 50);
        }

        public override void KeyPressed() // key event
        {

        }
    }
}

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

        public override void Setup() 
        {
            
        }

        public override void Draw() 
        {
            // test code
            graphics.Clear(Color.Black);
            graphics.FillRectangle(snakeBrush, MousePosition.X, MousePosition.Y, 50, 50);
        }

        public override void KeyPressed() 
        {

        }
    }
}

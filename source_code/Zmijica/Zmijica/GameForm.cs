using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zmijica
{
    public abstract partial class GameForm : Form
    {
        /******** FOR Game SUBCLASS **********/

        public abstract void Setup();
        public abstract void Draw();
        public abstract void KeyPressed();

        /****************** FORM CONTROL ***********************/

        protected Brush snakeBrush = new SolidBrush(Color.Green);
        protected Graphics graphics;

        public GameForm()
        {
            InitializeComponent();
            graphics = CreateGraphics();
            Application.Idle += HandleIdle;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Setup();
        }

        void HandleIdle(object sender, EventArgs e)
        {
            Draw();
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            KeyPressed();
        }
    }
}

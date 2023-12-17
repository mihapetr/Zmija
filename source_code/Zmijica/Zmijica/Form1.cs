using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zmijica
{
    public partial class Form1 : Form
    {
        Brush snakeBrush = new SolidBrush(Color.GreenYellow);
        Graphics g;

        public Form1()
        {
            InitializeComponent();
            g = CreateGraphics();
        }

        private void Setup(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            g.FillRectangle(snakeBrush,100,100,50,50);
            Thread t = new Thread(new ThreadStart(Draw));
            t.Start();
        }

        private void Draw()
        {
            BackColor = Color.Black;
            int x = MousePosition.X;
            int y = MousePosition.Y;
            g.FillRectangle(snakeBrush, x, y, 30, 30);
        }
    }
}

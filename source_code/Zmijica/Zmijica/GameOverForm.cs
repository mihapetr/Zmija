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
    public partial class GameOverForm : Form
    {
        public GameOverForm(int score)
        {
            InitializeComponent();
            this.Size = new Size(400, 250);
            labelGameOver.Width = labelScore.Width = labelScoreValue.Width = this.Width - 15;
            labelGameOver.Left = labelScore.Left = labelScoreValue.Left = 0;
            labelScoreValue.Text = score.ToString("00000000");
            buttonMenu.Left = (Width - 15 - buttonMenu.Width) / 2;
        }

        private void buttonMenu_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

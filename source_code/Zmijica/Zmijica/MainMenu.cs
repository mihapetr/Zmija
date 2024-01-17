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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            Game game = new Game();
            this.Hide();
            game.ShowDialog();
            this.Show();
        }

        private void buttonOptions_Click(object sender, EventArgs e)
        {
            OptionsScreen options = new OptionsScreen();
            this.Hide();
            options.ShowDialog();
            this.Show();
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            HelpScreen help = new HelpScreen();
            this.Hide();
            help.ShowDialog();
            this.Show();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GameVersus game = new GameVersus();
            this.Hide();
            game.ShowDialog();
            this.Show();
        }
    }
}

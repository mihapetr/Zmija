using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zmijica
{
    /// <summary>
    /// Preference korisnika o kontrolama tipkovnice.
    /// </summary>
    public class ControlSettings
    {
        public Keys up, down, left, right, tpEdgeActivator, tpSelfActovator;
        
        /// <summary>
        /// Postavlja default kontrole.
        /// </summary>
        public ControlSettings()
        {
            up = Keys.W;
            down = Keys.S;
            left = Keys.A;
            right = Keys.D;
            tpEdgeActivator = Keys.Shift;
            tpSelfActovator = Keys.Space;
        }

        /// <summary>
        /// Povezuje funkcionalnost preko aliasa s danim <c>Keys</c> kodom.
        /// </summary>
        /// <param name="buttonFunction"></param>
        /// <param name="keyCode"></param>
        public void setButton(string buttonFunction, Keys keyCode)
        {
            switch (buttonFunction)
            {
                case "up":
                    up = keyCode;
                    break;
                case "down":
                    down = keyCode;
                    break;
                case "left":
                    left = keyCode;
                    break;
                case "right":
                    right = keyCode;
                    break;
                case "edge":
                    tpEdgeActivator = keyCode;
                    break;
                case "self":
                    tpSelfActovator = keyCode;
                    break;
                default:
                    break;
            }
        }
    }

    public partial class MainMenu : Form
    {
        ControlSettings cSettings = new ControlSettings();

        public MainMenu()
        {
            InitializeComponent();
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            Game game = new Game(cSettings);
            this.Hide();
            game.ShowDialog();
            this.Show();
        }

        private void buttonOptions_Click(object sender, EventArgs e)
        {
            OptionsScreen options = new OptionsScreen(cSettings);
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
            GameVersus game = new GameVersus(cSettings);
            this.Hide();
            game.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GameVersus game = new GameVersus(cSettings);
            this.Hide();
            game.ShowDialog();
            this.Show();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zmijica
{

    public partial class HelpScreen : Form
    {
        ControlSettings cSettings;

        public HelpScreen(ControlSettings sts)
        {
            InitializeComponent();
            cSettings = sts;

            label1.Text = "---------------- Snake Game Help --------------------------------------------\r\n\r\n" +
                "Welcome to the pixelated version of Snake!\r\n\r\n" +
                "Controls:" + "\r\n" +
                "-up: " + cSettings.up.ToString() + "\r\n" +
                "-left: " + cSettings.left.ToString() + "\r\n" +
                "-right: " + cSettings.right.ToString() + "\r\n" +
                "-down: " + cSettings.down.ToString() + "\r\n" +
                "-teleports snake to the nearest wall in that direction: " + cSettings.tpEdgeActivator.ToString() + "+direction:" + "\r\n" +
                "-teleports snake to the nearest snake element \n\t in that direction: " + cSettings.tpSelfActovator.ToString() + "+direction" + "\r\n" +
                 "teleports snake for n tiles in that direction: " + "-n(number)+direction" + "\r\n" +
                "\r\n\r\n" +
                "Rules:" + "\r\n" + "1. The snake grows longer by eating food.\r\n" +
                "2. Avoid eating yourself or hitting the walls.\r\n" +
                "3. Poisonous food shrinks the snake.\r\n" +
                "4. Food appears randomly on the map.\r\n" +
                "5. Achieve the goal length to advance to the next stage.\r\n\r\n" +
                "Tips:\r\n" +
                "- Navigate through the stages to reach higher levels.\r\n\r\n" +
                "- Powerup can make you go faster or you can go trough yourself.\r\n\r\n" +
                "Good luck and enjoy playing Snake!" +
                "\r\n------------------------------------------------------------------------------------ ";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            MainMenu main = new MainMenu();
            this.Hide();
            main.ShowDialog();
            this.Show();
        }
    }
}

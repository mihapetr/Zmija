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
    public partial class HelpScreen : Form
    {
        public HelpScreen()
        {
            InitializeComponent();
            label1.Text = "---------------- Snake Game Help --------------------------------------------\r\n\r\n" +
                "Welcome to the pixelated version of Snake!\r\n\r\n" +
                "Controls:" + "\r\n" +
                "-up:" + "\r\n" +
                "-left:" + "\r\n" +
                "-right:" + "\r\n" +
                "-down:" + "\r\n" + "\r\n" +
                "-shift+direction:" + " teleports snake to the nearest wall in that direction" + "\r\n" +
                "-space+direction:" + " teleports snake to the nearest snake element \n\t in that direction" + "\r\n" +
                "-n(number)+direction:" + "teleports snake for n tiles in that direction" + "\r\n" +
                "\r\n\r\n" +
                "Rules:" + "\r\n" + "1. The snake grows longer by eating food.\r\n" +
                "2. Avoid eating yourself or hitting the walls.\r\n" +
                "3. Poisonous food shrinks the snake.\r\n" +
                "4. Food appears randomly on the map.\r\n" +
                "5. Achieve the goal length to advance to the next stage.\r\n\r\n" +
                "Tips:\r\n" +
                "- Navigate through the stages to reach higher levels.\r\n\r\n" +
                "Good luck and enjoy playing Snake!" +
                "\r\n------------------------------------------------------------------------------------ ";
            
        }
    }
}

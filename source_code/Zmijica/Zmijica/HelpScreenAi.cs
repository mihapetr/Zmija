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

    public partial class HelpScreenAi : Form
    {
        ControlSettings cSettings;

        public HelpScreenAi(ControlSettings sts)
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
                "\r\n\r\n" +
                "Rules:" + "\r\n" + "1. The snake grows longer by eating food.\r\n" +
                "2. Avoid eating yourself or hitting the walls.\r\n" +
                "3. you need to colect as much points as you can.\r\n" +
                "4. Food appears randomly on the map.\r\n\r\n" +
                "Tips:\r\n" +
                "- Powerup can make you go faster or you can go trough yourself.\r\n\r\n" +
                "Good luck and enjoy playing Snake!" +
                "\r\n------------------------------------------------------------------------------------ ";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}

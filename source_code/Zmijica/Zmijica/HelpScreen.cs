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
                "-down:" + "\r\n" +
                "\r\n\r\n" +
                "Rules:\r\n1. The snake grows longer by eating food.\r\n" +
                "2. Avoid eating yourself or hitting the walls.\r\n" +
                "3. Poisonous food shrinks the snake.\r\n" +
                "4. Food appears randomly on the map.\r\n" +
                "5. There are 3 stages (maps): 1, 2, 3, 1, 2, 3, ...\r\n" +
                "6. No level restrictions, but every three stages, difficulty increases.\r\n" +
                "7. Achieve the goal length to advance to the next stage.\r\n\r\n" +
                "Tips:\r\n- Pay attention to your snake's length and the stage.\r\n" +
                "- Be strategic when approaching poisonous food.\r\n" +
                "- Navigate through the stages to reach higher levels.\r\n\r\n" +
                "Good luck and enjoy playing Snake!" +
                "\r\n------------------------------------------------------------------------------------\r\n\r\n ";
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}

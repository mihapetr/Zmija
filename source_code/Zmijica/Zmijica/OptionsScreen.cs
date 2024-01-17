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
    public partial class OptionsScreen : Form
    {
        ControlSettings cSettings;

        public OptionsScreen(ControlSettings sts)
        {
            InitializeComponent();
            cSettings = sts;
            labelUp.Text = cSettings.up.ToString();
            labelDown.Text = cSettings.down.ToString();
            labelLeft.Text = cSettings.left.ToString();
            labelRight.Text = cSettings.right.ToString();
            labelSelf.Text = cSettings.tpSelfActovator.ToString();
            labelEdge.Text = cSettings.tpEdgeActivator.ToString();
        }

        private void labelUp_Click(object sender, EventArgs e)
        {

        }

        private void labelRight_Click(object sender, EventArgs e)
        {

        }

        private void labelLeft_Click(object sender, EventArgs e)
        {

        }

        private void labelDown_Click(object sender, EventArgs e)
        {

        }

        private void labelEdge_Click(object sender, EventArgs e)
        {

        }

        private void labelSelf_Click(object sender, EventArgs e)
        {

        }
    }
}

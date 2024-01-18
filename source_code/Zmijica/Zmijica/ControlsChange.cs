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
    public partial class ControlsChange : Form
    {
        string buttonFunction;
        ControlSettings cSettings;

        public ControlsChange(ControlSettings sts, string bFunction)
        {
            InitializeComponent();
            buttonFunction = bFunction;
            cSettings = sts;
        }

        private void ControlsChange_KeyDown(object sender, KeyEventArgs e)
        {
            cSettings.setButton(buttonFunction, e.KeyCode);
            Close();
        }
    }
}

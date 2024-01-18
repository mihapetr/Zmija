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

        /// <summary>
        /// Nakon zatvaranaj dijaloga za promjenu kontrole se prikazuje rezultat promjene.
        /// </summary>
        void RefreshView()
        {
            labelUp.Text = cSettings.up.ToString();
            labelDown.Text = cSettings.down.ToString();
            labelLeft.Text = cSettings.left.ToString();
            labelRight.Text = cSettings.right.ToString();
            labelSelf.Text = cSettings.tpSelfActovator.ToString();
            labelEdge.Text = cSettings.tpEdgeActivator.ToString();
        }

        /// <summary>
        /// Metoda često korištene sekvence naredbi kod promjene kontrole.
        /// </summary>
        /// <param name="bFunction"></param>
        void UpdateTasks(string bFunction)
        {
            ControlsChange cc = new ControlsChange(cSettings, bFunction);
            cc.ShowDialog();
            RefreshView();
        }

        private void labelUp_Click(object sender, EventArgs e)
        {
            UpdateTasks("up");
        }

        private void labelRight_Click(object sender, EventArgs e)
        {
            UpdateTasks("right");
        }

        private void labelLeft_Click(object sender, EventArgs e)
        {
            UpdateTasks("left");
        }

        private void labelDown_Click(object sender, EventArgs e)
        {
            UpdateTasks("down");
        }

        private void labelEdge_Click(object sender, EventArgs e)
        {
            UpdateTasks("edge");
        }

        private void labelSelf_Click(object sender, EventArgs e)
        {
            UpdateTasks("self");
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

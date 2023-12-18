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
    /// <summary>
    /// Klasa napravljena kao sloj između <see cref="Form"/> i <see cref="Game"/>.
    /// 
    /// </summary>
    public abstract partial class GameForm : Form
    {
        /******** FOR Game SUBCLASS **********/

        protected Keys KeyCode;

        /// <summary>
        /// Standardna funkcija kod programiranja igara.
        /// Poziva se kod Load eventa forme. 
        /// </summary>
        public abstract void Setup();
        
        /// <summary>
        /// Standardna funkcija kod programiranja igara.
        /// Poziva se prije iscrtavanja svakog framea.
        /// </summary>
        public abstract void Draw();
        
        /// <summary>
        /// Standardna funkcija kod programiranja igara.
        /// Poziva se kad korisnik aplikacije pritisne tipku na tipkovnici.
        /// Varijabla <see cref="KeyCode"/> sadrži kod pritisnute tipke.
        /// </summary>
        public virtual void KeyPressed() { }
        
        /// <summary>
        /// Standardna funkcija kod programiranja igara.
        /// Poziva se kad korisnik aplikacije pritisne tipku na tipkovnici.
        /// Varijabla <see cref="KeyCode"/> sadrži kod otpuštene tipke.
        /// </summary>
        public virtual void KeyReleased() { }

        /****************** FORM CONTROL ***********************/

        protected Brush snakeBrush = new SolidBrush(Color.Green);
        protected Graphics graphics;
        // deprecated:
        //static readonly int FPS = 58;

        public GameForm()
        {
            InitializeComponent();
            graphics = CreateGraphics();
            Application.Idle += HandleIdle;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Setup();
        }

        void HandleIdle(object sender, EventArgs e)
        {
            SuspendLayout();
            Draw();
            ResumeLayout();
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            KeyCode = e.KeyCode;
            KeyPressed();
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            KeyCode = e.KeyCode;
            KeyReleased();
        }
    }
}

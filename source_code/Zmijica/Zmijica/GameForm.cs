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

        
        protected Graphics graphics;
        protected TableLayoutPanel screen;
        protected int width;
        public Varijable varijable;

        protected int FPS
        {
            set
            {
                timer1.Interval = 1000 / value;
            }
        }

        public GameForm()
        {
            InitializeComponent();
            graphics = CreateGraphics();
            // Application.Idle += HandleIdle;
            timer1.Start();
            varijable = new Varijable();
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

        /// <summary>
        /// Kreira piksele za prikaz elemenata igre. width je širina i visina u velikim pikselima, odnosno kvadratima.
        /// </summary>
        /// <param name="width"></param>
        protected void InitializeScreen(int width)
        {
            this.width = width;
            varijable.width = width;
            int height = width;
            SuspendLayout();
            screen = new System.Windows.Forms.TableLayoutPanel();
            screen.ColumnCount = width;
            screen.RowCount = width;
            screen.Dock = System.Windows.Forms.DockStyle.Fill;
            screen.Location = new System.Drawing.Point(0, 0);
            screen.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            screen.Size = new System.Drawing.Size(704, 681);
            for (int i = 0; i < width; i++)
            {
                screen.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100/width));
            }
            for (int i = 0; i < height; i++)
            {
                screen.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100 / height));
            }
            Controls.Add(screen);

            // dodavanje panela u svaki cell
            Panel p;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    p = new Panel();
                    // details
                    p.Dock = DockStyle.Fill;
                    p.Margin = new Padding(0);
                    p.Location = new Point(0, 0);
                    // adding
                    screen.Controls.Add(p, i, j);
                }
            }
            ResumeLayout();
        }

        protected void ClearScreen()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    screen.GetControlFromPosition(i, j).BackColor = Color.Black;
                }
            }
        }

        /// <summary>
        /// Crta listu točaka (int,int) i crta ih u boji color
        /// </summary>
        /// <param name="pts"></param>
        /// <param name="color"></param>
        public void DrawList(List<Point> pts, Color color)
        {
            foreach (Point p in pts)
            {
                if (p.X >= width || p.Y >= width) throw new Exception("Neki od danih piksela je izvan okvira ekrana.");
                screen.GetControlFromPosition(p.X, p.Y).BackColor = color;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // za maksimalnu performansu se koristi Idle event.
            // Ovdje je posuđen handler za to jer za kretanje zmije želimo dosta niski refresh rate.
            HandleIdle(sender, e);
        }
    }
}

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

        
        protected Dictionary<int, TableLayoutPanel> screens = new Dictionary<int, TableLayoutPanel>();
        protected TableLayoutPanel activeScreen;
        public Varijable varijable = new Varijable();  // contains game variables

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
            // Application.Idle += HandleIdle;
            timer1.Start();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Setup();
        }

        void HandleIdle(object sender, EventArgs e)
        {
            Draw();
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

        protected void GetScreen(int width)
        {
            TableLayoutPanel screen;
            if (screens.TryGetValue(width, out screen))
            {
                screen.BringToFront();
                activeScreen = screen;
            }
            else { 
                screens.Add(width, (screen = InitializeScreen(width)));
                //Controls.Add(screen);
                activeScreen = screen;
                screen.BringToFront();
            }
            varijable.width = width;
        }

        /// <summary>
        /// Kreira piksele za prikaz elemenata igre. width je širina i visina u velikim pikselima, odnosno kvadratima.
        /// </summary>
        /// <param name="width"></param>
        protected TableLayoutPanel InitializeScreen(int width)
        {
            //if (Controls.Contains(screen)) Controls.Remove(screen);

            TableLayoutPanel screen = new TableLayoutPanel();
            int height = width;
            SuspendLayout();
            screen = new System.Windows.Forms.TableLayoutPanel();
            screen.ColumnCount = width;
            screen.RowCount = width;
            //screen.Dock = System.Windows.Forms.DockStyle.Fill;
            screen.Location = new System.Drawing.Point(0, 0);
            //screen.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            screen.RowStyles.Clear();
            screen.Size = new Size(585, 585);
            float wPercent = screen.Width / width;
            float hPercent = screen.Height / width;
            
            for (int i = 0; i < width; i++)
            {
                if((i % 50) != 0)
                    screen.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, wPercent));
                else
                    screen.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, wPercent));
            }
            for (int i = 0; i < width; i++)
            {
                if((i % 50) != 0)
                    screen.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, hPercent));
                else
                    screen.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, hPercent));
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
            return screen;
        }

        protected void ClearScreen()
        {
            for (int i = 0; i < varijable.width; i++)
            {
                for (int j = 0; j < varijable.width; j++)
                {
                    activeScreen.GetControlFromPosition(i, j).BackColor = Color.Black;
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
                if (p.X >= varijable.width || p.Y >= varijable.width) throw new Exception("Neki od danih piksela je izvan okvira ekrana.");
                activeScreen.GetControlFromPosition(p.X, p.Y).BackColor = color;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // za maksimalnu performansu se koristi Idle event.
            // Ovdje je posuđen handler za to jer za kretanje zmije želimo dosta niski refresh rate.
            HandleIdle(sender, e);
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
            Dispose();
        }
    }
}

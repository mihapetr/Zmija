namespace Zmijica
{
    partial class GameOverForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelGameOver = new System.Windows.Forms.Label();
            this.labelScore = new System.Windows.Forms.Label();
            this.labelScoreValue = new System.Windows.Forms.Label();
            this.buttonMenu = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelGameOver
            // 
            this.labelGameOver.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelGameOver.ForeColor = System.Drawing.SystemColors.Control;
            this.labelGameOver.Location = new System.Drawing.Point(65, 9);
            this.labelGameOver.Name = "labelGameOver";
            this.labelGameOver.Size = new System.Drawing.Size(240, 31);
            this.labelGameOver.TabIndex = 1;
            this.labelGameOver.Text = "Game Over";
            this.labelGameOver.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelScore
            // 
            this.labelScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelScore.ForeColor = System.Drawing.SystemColors.Control;
            this.labelScore.Location = new System.Drawing.Point(110, 68);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(153, 31);
            this.labelScore.TabIndex = 2;
            this.labelScore.Text = "Score:";
            this.labelScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelScoreValue
            // 
            this.labelScoreValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelScoreValue.ForeColor = System.Drawing.SystemColors.Control;
            this.labelScoreValue.Location = new System.Drawing.Point(137, 99);
            this.labelScoreValue.Name = "labelScoreValue";
            this.labelScoreValue.Size = new System.Drawing.Size(100, 31);
            this.labelScoreValue.TabIndex = 3;
            this.labelScoreValue.Text = "000";
            this.labelScoreValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonMenu
            // 
            this.buttonMenu.BackColor = System.Drawing.Color.White;
            this.buttonMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMenu.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonMenu.Location = new System.Drawing.Point(133, 156);
            this.buttonMenu.Name = "buttonMenu";
            this.buttonMenu.Size = new System.Drawing.Size(104, 28);
            this.buttonMenu.TabIndex = 4;
            this.buttonMenu.Text = "Main Menu";
            this.buttonMenu.UseVisualStyleBackColor = false;
            this.buttonMenu.Click += new System.EventHandler(this.buttonMenu_Click);
            // 
            // GameOverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.ClientSize = new System.Drawing.Size(384, 216);
            this.ControlBox = false;
            this.Controls.Add(this.buttonMenu);
            this.Controls.Add(this.labelScoreValue);
            this.Controls.Add(this.labelScore);
            this.Controls.Add(this.labelGameOver);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameOverForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Zmijica";
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Label labelGameOver;
        protected System.Windows.Forms.Label labelScore;
        protected System.Windows.Forms.Label labelScoreValue;
        private System.Windows.Forms.Button buttonMenu;
    }
}
using System.Drawing;
using System.Windows.Forms;

namespace Zmijica
{
    partial class GameForm
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelScore = new System.Windows.Forms.Label();
            this.labelLength = new System.Windows.Forms.Label();
            this.labelLevel = new System.Windows.Forms.Label();
            this.labelStage = new System.Windows.Forms.Label();
            this.screenContainer = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.labelSkipAmount = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(704, 681);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(338, 339);
            this.panel1.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labelScore
            // 
            this.labelScore.AutoSize = true;
            this.labelScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelScore.ForeColor = System.Drawing.SystemColors.Control;
            this.labelScore.Location = new System.Drawing.Point(2, 608);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(190, 31);
            this.labelScore.TabIndex = 0;
            this.labelScore.Text = "Score: 000000";
            // 
            // labelLength
            // 
            this.labelLength.AutoSize = true;
            this.labelLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelLength.ForeColor = System.Drawing.SystemColors.Control;
            this.labelLength.Location = new System.Drawing.Point(2, 641);
            this.labelLength.Name = "labelLength";
            this.labelLength.Size = new System.Drawing.Size(202, 31);
            this.labelLength.TabIndex = 1;
            this.labelLength.Text = "Snake length: 0";
            // 
            // labelLevel
            // 
            this.labelLevel.AutoSize = true;
            this.labelLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelLevel.ForeColor = System.Drawing.SystemColors.Control;
            this.labelLevel.Location = new System.Drawing.Point(423, 608);
            this.labelLevel.Name = "labelLevel";
            this.labelLevel.Size = new System.Drawing.Size(154, 31);
            this.labelLevel.TabIndex = 2;
            this.labelLevel.Text = "Level: 0001";
            // 
            // labelStage
            // 
            this.labelStage.AutoSize = true;
            this.labelStage.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelStage.ForeColor = System.Drawing.SystemColors.Control;
            this.labelStage.Location = new System.Drawing.Point(462, 641);
            this.labelStage.Name = "labelStage";
            this.labelStage.Size = new System.Drawing.Size(115, 31);
            this.labelStage.TabIndex = 3;
            this.labelStage.Text = "Stage: 1";
            // 
            // screenContainer
            // 
            this.screenContainer.AutoSize = true;
            this.screenContainer.ColumnCount = 1;
            this.screenContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.screenContainer.Location = new System.Drawing.Point(122, 50);
            this.screenContainer.Name = "screenContainer";
            this.screenContainer.RowCount = 1;
            this.screenContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.screenContainer.Size = new System.Drawing.Size(600, 720);
            this.screenContainer.TabIndex = 4;
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(283, 648);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(25, 24);
            this.pictureBox.TabIndex = 4;
            this.pictureBox.TabStop = false;
            // 
            // labelSkipAmount
            // 
            this.labelSkipAmount.AutoSize = true;
            this.labelSkipAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelSkipAmount.ForeColor = System.Drawing.Color.Gold;
            this.labelSkipAmount.Location = new System.Drawing.Point(314, 648);
            this.labelSkipAmount.Name = "labelSkipAmount";
            this.labelSkipAmount.Size = new System.Drawing.Size(20, 24);
            this.labelSkipAmount.TabIndex = 5;
            this.labelSkipAmount.Text = "9";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(584, 681);
            this.Controls.Add(this.labelSkipAmount);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.labelStage);
            this.Controls.Add(this.labelLevel);
            this.Controls.Add(this.labelLength);
            this.Controls.Add(this.labelScore);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Zmijica";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameForm_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyUp);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;    // korstio se samo kao uzor za dinamičku kreaciju
        private Panel panel1;   // korstio se samo kao uzor za dinamičku kreaciju
        protected Timer timer1; // za kontrolu FPS-a igrice
        protected Label labelScore;
        protected Label labelLength;
        protected Label labelLevel;
        protected Label labelStage;
        protected TableLayoutPanel screenContainer;
        protected PictureBox pictureBox;
        protected Label labelSkipAmount;
    }
}
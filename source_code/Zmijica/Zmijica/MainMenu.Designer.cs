﻿namespace Zmijica
{
    partial class MainMenu
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
            this.buttonPlay = new System.Windows.Forms.Button();
            this.buttonOptions = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonPlay
            // 
            this.buttonPlay.BackColor = System.Drawing.Color.ForestGreen;
            this.buttonPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPlay.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonPlay.ForeColor = System.Drawing.Color.Indigo;
            this.buttonPlay.Location = new System.Drawing.Point(312, 266);
            this.buttonPlay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(139, 34);
            this.buttonPlay.TabIndex = 5;
            this.buttonPlay.Text = "Play Game";
            this.buttonPlay.UseVisualStyleBackColor = false;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // buttonOptions
            // 
            this.buttonOptions.BackColor = System.Drawing.Color.ForestGreen;
            this.buttonOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOptions.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonOptions.ForeColor = System.Drawing.Color.Indigo;
            this.buttonOptions.Location = new System.Drawing.Point(312, 329);
            this.buttonOptions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonOptions.Name = "buttonOptions";
            this.buttonOptions.Size = new System.Drawing.Size(139, 34);
            this.buttonOptions.TabIndex = 6;
            this.buttonOptions.Text = "Options";
            this.buttonOptions.UseVisualStyleBackColor = false;
            this.buttonOptions.Click += new System.EventHandler(this.buttonOptions_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.Color.ForestGreen;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonExit.ForeColor = System.Drawing.Color.Indigo;
            this.buttonExit.Location = new System.Drawing.Point(312, 453);
            this.buttonExit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(139, 34);
            this.buttonExit.TabIndex = 7;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonHelp
            // 
            this.buttonHelp.BackColor = System.Drawing.Color.ForestGreen;
            this.buttonHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonHelp.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonHelp.ForeColor = System.Drawing.Color.Indigo;
            this.buttonHelp.Location = new System.Drawing.Point(312, 390);
            this.buttonHelp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(139, 34);
            this.buttonHelp.TabIndex = 8;
            this.buttonHelp.Text = "Help";
            this.buttonHelp.UseVisualStyleBackColor = false;
            this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImage = global::Zmijica.Properties.Resources.kobre;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(779, 838);
            this.Controls.Add(this.buttonHelp);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonOptions);
            this.Controls.Add(this.buttonPlay);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zmijica";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Button buttonOptions;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonHelp;
    }
}
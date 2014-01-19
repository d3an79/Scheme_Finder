namespace Scheme_Finder
{
    partial class MainForm
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
            this.btn_Source = new System.Windows.Forms.Button();
            this.btn_Destination = new System.Windows.Forms.Button();
            this.txt_DisplaySource = new System.Windows.Forms.TextBox();
            this.txt_DisplayDestination = new System.Windows.Forms.TextBox();
            this.txt_Tolerance = new System.Windows.Forms.TextBox();
            this.txt_Length = new System.Windows.Forms.TextBox();
            this.txt_Score = new System.Windows.Forms.TextBox();
            this.btn_Start = new System.Windows.Forms.Button();
            this.btn_ShowScheme = new System.Windows.Forms.Button();
            this.lvw_SchemeSummary = new System.Windows.Forms.ListView();
            this.lvw_Scheme = new System.Windows.Forms.ListView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Source
            // 
            this.btn_Source.Location = new System.Drawing.Point(12, 27);
            this.btn_Source.Name = "btn_Source";
            this.btn_Source.Size = new System.Drawing.Size(75, 23);
            this.btn_Source.TabIndex = 0;
            this.btn_Source.Text = "Source";
            this.btn_Source.UseVisualStyleBackColor = true;
            this.btn_Source.Click += new System.EventHandler(this.btn_Source_Click);
            // 
            // btn_Destination
            // 
            this.btn_Destination.Location = new System.Drawing.Point(11, 78);
            this.btn_Destination.Name = "btn_Destination";
            this.btn_Destination.Size = new System.Drawing.Size(75, 23);
            this.btn_Destination.TabIndex = 1;
            this.btn_Destination.Text = "Destination";
            this.btn_Destination.UseVisualStyleBackColor = true;
            this.btn_Destination.Click += new System.EventHandler(this.btn_Destination_Click);
            // 
            // txt_DisplaySource
            // 
            this.txt_DisplaySource.Enabled = false;
            this.txt_DisplaySource.Location = new System.Drawing.Point(116, 27);
            this.txt_DisplaySource.Name = "txt_DisplaySource";
            this.txt_DisplaySource.Size = new System.Drawing.Size(306, 20);
            this.txt_DisplaySource.TabIndex = 2;
            this.txt_DisplaySource.TabStop = false;
            // 
            // txt_DisplayDestination
            // 
            this.txt_DisplayDestination.Enabled = false;
            this.txt_DisplayDestination.Location = new System.Drawing.Point(118, 78);
            this.txt_DisplayDestination.Name = "txt_DisplayDestination";
            this.txt_DisplayDestination.Size = new System.Drawing.Size(304, 20);
            this.txt_DisplayDestination.TabIndex = 3;
            this.txt_DisplayDestination.TabStop = false;
            // 
            // txt_Tolerance
            // 
            this.txt_Tolerance.Location = new System.Drawing.Point(173, 133);
            this.txt_Tolerance.Name = "txt_Tolerance";
            this.txt_Tolerance.Size = new System.Drawing.Size(100, 20);
            this.txt_Tolerance.TabIndex = 3;
            // 
            // txt_Length
            // 
            this.txt_Length.Location = new System.Drawing.Point(11, 133);
            this.txt_Length.Name = "txt_Length";
            this.txt_Length.Size = new System.Drawing.Size(100, 20);
            this.txt_Length.TabIndex = 2;
            // 
            // txt_Score
            // 
            this.txt_Score.Location = new System.Drawing.Point(15, 190);
            this.txt_Score.Name = "txt_Score";
            this.txt_Score.Size = new System.Drawing.Size(100, 20);
            this.txt_Score.TabIndex = 4;
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(173, 190);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(100, 23);
            this.btn_Start.TabIndex = 5;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // btn_ShowScheme
            // 
            this.btn_ShowScheme.Location = new System.Drawing.Point(312, 190);
            this.btn_ShowScheme.Name = "btn_ShowScheme";
            this.btn_ShowScheme.Size = new System.Drawing.Size(110, 23);
            this.btn_ShowScheme.TabIndex = 6;
            this.btn_ShowScheme.Text = "Show Scheme";
            this.btn_ShowScheme.UseVisualStyleBackColor = true;
            this.btn_ShowScheme.Click += new System.EventHandler(this.btn_ShowScheme_Click);
            // 
            // lvw_SchemeSummary
            // 
            this.lvw_SchemeSummary.Location = new System.Drawing.Point(15, 239);
            this.lvw_SchemeSummary.Name = "lvw_SchemeSummary";
            this.lvw_SchemeSummary.Size = new System.Drawing.Size(556, 97);
            this.lvw_SchemeSummary.TabIndex = 7;
            this.lvw_SchemeSummary.UseCompatibleStateImageBehavior = false;
            // 
            // lvw_Scheme
            // 
            this.lvw_Scheme.Location = new System.Drawing.Point(15, 368);
            this.lvw_Scheme.Name = "lvw_Scheme";
            this.lvw_Scheme.Size = new System.Drawing.Size(556, 256);
            this.lvw_Scheme.TabIndex = 10;
            this.lvw_Scheme.UseCompatibleStateImageBehavior = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(598, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Scheme length:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(170, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Opposite side tolerance";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Target Score";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 698);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lvw_Scheme);
            this.Controls.Add(this.lvw_SchemeSummary);
            this.Controls.Add(this.btn_ShowScheme);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.txt_Score);
            this.Controls.Add(this.txt_Length);
            this.Controls.Add(this.txt_Tolerance);
            this.Controls.Add(this.txt_DisplayDestination);
            this.Controls.Add(this.txt_DisplaySource);
            this.Controls.Add(this.btn_Destination);
            this.Controls.Add(this.btn_Source);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Scheme Finder";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Source;
        private System.Windows.Forms.Button btn_Destination;
        private System.Windows.Forms.TextBox txt_DisplaySource;
        private System.Windows.Forms.TextBox txt_DisplayDestination;
        private System.Windows.Forms.TextBox txt_Tolerance;
        private System.Windows.Forms.TextBox txt_Length;
        private System.Windows.Forms.TextBox txt_Score;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Button btn_ShowScheme;
        private System.Windows.Forms.ListView lvw_SchemeSummary;
        private System.Windows.Forms.ListView lvw_Scheme;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}


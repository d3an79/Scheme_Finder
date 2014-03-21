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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
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
            this.cmbo_ChartType = new System.Windows.Forms.ToolStripComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lbl_Info1 = new System.Windows.Forms.Label();
            this.lbl_Info2 = new System.Windows.Forms.Label();
            this.lbl_Info3 = new System.Windows.Forms.Label();
            this.lbl_Info4 = new System.Windows.Forms.Label();
            this.lbl_Info5 = new System.Windows.Forms.Label();
            this.txt_LowLimit = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
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
            this.btn_Destination.Location = new System.Drawing.Point(12, 53);
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
            this.txt_DisplayDestination.Location = new System.Drawing.Point(116, 53);
            this.txt_DisplayDestination.Name = "txt_DisplayDestination";
            this.txt_DisplayDestination.Size = new System.Drawing.Size(304, 20);
            this.txt_DisplayDestination.TabIndex = 3;
            this.txt_DisplayDestination.TabStop = false;
            // 
            // txt_Tolerance
            // 
            this.txt_Tolerance.Location = new System.Drawing.Point(227, 100);
            this.txt_Tolerance.Name = "txt_Tolerance";
            this.txt_Tolerance.Size = new System.Drawing.Size(100, 20);
            this.txt_Tolerance.TabIndex = 4;
            // 
            // txt_Length
            // 
            this.txt_Length.Location = new System.Drawing.Point(12, 100);
            this.txt_Length.Name = "txt_Length";
            this.txt_Length.Size = new System.Drawing.Size(100, 20);
            this.txt_Length.TabIndex = 2;
            // 
            // txt_Score
            // 
            this.txt_Score.Location = new System.Drawing.Point(121, 100);
            this.txt_Score.Name = "txt_Score";
            this.txt_Score.Size = new System.Drawing.Size(100, 20);
            this.txt_Score.TabIndex = 3;
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(15, 129);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(100, 23);
            this.btn_Start.TabIndex = 5;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // btn_ShowScheme
            // 
            this.btn_ShowScheme.Location = new System.Drawing.Point(122, 210);
            this.btn_ShowScheme.Name = "btn_ShowScheme";
            this.btn_ShowScheme.Size = new System.Drawing.Size(99, 23);
            this.btn_ShowScheme.TabIndex = 7;
            this.btn_ShowScheme.Text = "Show Scheme";
            this.btn_ShowScheme.UseVisualStyleBackColor = true;
            this.btn_ShowScheme.Click += new System.EventHandler(this.btn_ShowScheme_Click);
            // 
            // lvw_SchemeSummary
            // 
            this.lvw_SchemeSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvw_SchemeSummary.FullRowSelect = true;
            this.lvw_SchemeSummary.HideSelection = false;
            this.lvw_SchemeSummary.Location = new System.Drawing.Point(15, 258);
            this.lvw_SchemeSummary.MultiSelect = false;
            this.lvw_SchemeSummary.Name = "lvw_SchemeSummary";
            this.lvw_SchemeSummary.Size = new System.Drawing.Size(886, 304);
            this.lvw_SchemeSummary.TabIndex = 9;
            this.lvw_SchemeSummary.UseCompatibleStateImageBehavior = false;
            this.lvw_SchemeSummary.View = System.Windows.Forms.View.Details;
            this.lvw_SchemeSummary.SelectedIndexChanged += new System.EventHandler(this.lvw_SchemeSummary_SelectedIndexChanged);
            // 
            // lvw_Scheme
            // 
            this.lvw_Scheme.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvw_Scheme.Location = new System.Drawing.Point(15, 568);
            this.lvw_Scheme.Name = "lvw_Scheme";
            this.lvw_Scheme.Size = new System.Drawing.Size(886, 174);
            this.lvw_Scheme.TabIndex = 10;
            this.lvw_Scheme.UseCompatibleStateImageBehavior = false;
            this.lvw_Scheme.View = System.Windows.Forms.View.Details;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem,
            this.cmbo_ChartType});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(928, 27);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 23);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // cmbo_ChartType
            // 
            this.cmbo_ChartType.Name = "cmbo_ChartType";
            this.cmbo_ChartType.Size = new System.Drawing.Size(121, 23);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Scheme length:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(224, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Opposite side tol";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(118, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Target Score";
            // 
            // chart1
            // 
            chartArea1.AxisX.MinorGrid.Enabled = true;
            chartArea1.AxisX.MinorGrid.Interval = 1D;
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.LabelStyle.Interval = 50D;
            chartArea1.AxisY.MajorGrid.Interval = 50D;
            chartArea1.AxisY.MajorTickMark.Interval = 25D;
            chartArea1.AxisY.Maximum = 300D;
            chartArea1.AxisY.Minimum = 0D;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(444, 12);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(457, 221);
            this.chart1.TabIndex = 15;
            this.chart1.Text = "chart1";
            // 
            // lbl_Info1
            // 
            this.lbl_Info1.AutoSize = true;
            this.lbl_Info1.Location = new System.Drawing.Point(245, 129);
            this.lbl_Info1.Name = "lbl_Info1";
            this.lbl_Info1.Size = new System.Drawing.Size(35, 13);
            this.lbl_Info1.TabIndex = 16;
            this.lbl_Info1.Text = "label4";
            this.lbl_Info1.Visible = false;
            // 
            // lbl_Info2
            // 
            this.lbl_Info2.AutoSize = true;
            this.lbl_Info2.Location = new System.Drawing.Point(245, 154);
            this.lbl_Info2.Name = "lbl_Info2";
            this.lbl_Info2.Size = new System.Drawing.Size(35, 13);
            this.lbl_Info2.TabIndex = 17;
            this.lbl_Info2.Text = "label5";
            this.lbl_Info2.Visible = false;
            // 
            // lbl_Info3
            // 
            this.lbl_Info3.AutoSize = true;
            this.lbl_Info3.Location = new System.Drawing.Point(245, 180);
            this.lbl_Info3.Name = "lbl_Info3";
            this.lbl_Info3.Size = new System.Drawing.Size(35, 13);
            this.lbl_Info3.TabIndex = 18;
            this.lbl_Info3.Text = "label6";
            this.lbl_Info3.Visible = false;
            // 
            // lbl_Info4
            // 
            this.lbl_Info4.AutoSize = true;
            this.lbl_Info4.Location = new System.Drawing.Point(245, 205);
            this.lbl_Info4.Name = "lbl_Info4";
            this.lbl_Info4.Size = new System.Drawing.Size(35, 13);
            this.lbl_Info4.TabIndex = 19;
            this.lbl_Info4.Text = "label7";
            this.lbl_Info4.Visible = false;
            // 
            // lbl_Info5
            // 
            this.lbl_Info5.AutoSize = true;
            this.lbl_Info5.Location = new System.Drawing.Point(245, 230);
            this.lbl_Info5.Name = "lbl_Info5";
            this.lbl_Info5.Size = new System.Drawing.Size(35, 13);
            this.lbl_Info5.TabIndex = 20;
            this.lbl_Info5.Text = "label8";
            this.lbl_Info5.Visible = false;
            // 
            // txt_LowLimit
            // 
            this.txt_LowLimit.Location = new System.Drawing.Point(15, 215);
            this.txt_LowLimit.Name = "txt_LowLimit";
            this.txt_LowLimit.Size = new System.Drawing.Size(100, 20);
            this.txt_LowLimit.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 199);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Low score limit";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 754);
            this.Controls.Add(this.txt_LowLimit);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbl_Info5);
            this.Controls.Add(this.lbl_Info4);
            this.Controls.Add(this.lbl_Info3);
            this.Controls.Add(this.lbl_Info2);
            this.Controls.Add(this.lbl_Info1);
            this.Controls.Add(this.chart1);
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
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
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
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label lbl_Info1;
        private System.Windows.Forms.Label lbl_Info2;
        private System.Windows.Forms.Label lbl_Info3;
        private System.Windows.Forms.Label lbl_Info4;
        private System.Windows.Forms.Label lbl_Info5;
        private System.Windows.Forms.TextBox txt_LowLimit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripComboBox cmbo_ChartType;
    }
}


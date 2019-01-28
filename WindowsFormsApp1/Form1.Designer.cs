namespace WindowsFormsApp1
{
    partial class Form1
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
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.freqChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.samplChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.openBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.recordBtn = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.copyBtn = new System.Windows.Forms.Button();
            this.pasteBtn = new System.Windows.Forms.Button();
            this.playBtn = new System.Windows.Forms.Button();
            this.invFourierBtn = new System.Windows.Forms.Button();
            this.radioRect = new System.Windows.Forms.RadioButton();
            this.triRadio = new System.Windows.Forms.RadioButton();
            this.hamRadio = new System.Windows.Forms.RadioButton();
            this.filterBtn = new System.Windows.Forms.Button();
            this.filterValue = new System.Windows.Forms.NumericUpDown();
            this.hpRadio = new System.Windows.Forms.RadioButton();
            this.lpRadio = new System.Windows.Forms.RadioButton();
            this.filterBox = new System.Windows.Forms.GroupBox();
            this.aplWinCheck = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.thrdFourierBtn = new System.Windows.Forms.Button();
            this.thrdFilterBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.freqChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.samplChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterValue)).BeginInit();
            this.filterBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // freqChart
            // 
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.Name = "ChartArea1";
            this.freqChart.ChartAreas.Add(chartArea1);
            this.freqChart.Location = new System.Drawing.Point(198, 340);
            this.freqChart.Name = "freqChart";
            series1.ChartArea = "ChartArea1";
            series1.Name = "freqChart";
            this.freqChart.Series.Add(series1);
            this.freqChart.Size = new System.Drawing.Size(1054, 329);
            this.freqChart.TabIndex = 0;
            this.freqChart.Text = "freqChart";
            // 
            // samplChart
            // 
            chartArea2.AxisX.Minimum = 0D;
            chartArea2.Name = "ChartArea2";
            this.samplChart.ChartAreas.Add(chartArea2);
            this.samplChart.Location = new System.Drawing.Point(198, 12);
            this.samplChart.Name = "samplChart";
            series2.ChartArea = "ChartArea2";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Name = "Series1";
            this.samplChart.Series.Add(series2);
            this.samplChart.Size = new System.Drawing.Size(1054, 312);
            this.samplChart.TabIndex = 1;
            this.samplChart.Text = "chart2";
            this.samplChart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.samplChart_MouseDown);
            this.samplChart.MouseUp += new System.Windows.Forms.MouseEventHandler(this.samplChart_MouseUp);
            // 
            // openBtn
            // 
            this.openBtn.Location = new System.Drawing.Point(12, 12);
            this.openBtn.Name = "openBtn";
            this.openBtn.Size = new System.Drawing.Size(75, 23);
            this.openBtn.TabIndex = 2;
            this.openBtn.Text = "Open";
            this.openBtn.UseVisualStyleBackColor = true;
            this.openBtn.Click += new System.EventHandler(this.openBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(12, 41);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 3;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // recordBtn
            // 
            this.recordBtn.Location = new System.Drawing.Point(12, 70);
            this.recordBtn.Name = "recordBtn";
            this.recordBtn.Size = new System.Drawing.Size(75, 23);
            this.recordBtn.TabIndex = 4;
            this.recordBtn.Text = "Record";
            this.recordBtn.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Title = "Select Wave File";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(198, 684);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1054, 251);
            this.richTextBox1.TabIndex = 6;
            this.richTextBox1.Text = "";
            // 
            // deleteBtn
            // 
            this.deleteBtn.Location = new System.Drawing.Point(107, 534);
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(75, 23);
            this.deleteBtn.TabIndex = 7;
            this.deleteBtn.Text = "Cut";
            this.deleteBtn.UseVisualStyleBackColor = true;
            this.deleteBtn.Click += new System.EventHandler(this.deleteBtn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(95, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "FourierView";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.LightSkyBlue;
            this.button2.Location = new System.Drawing.Point(111, 408);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Selection";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(129, 392);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Mode";
            // 
            // copyBtn
            // 
            this.copyBtn.Location = new System.Drawing.Point(107, 476);
            this.copyBtn.Name = "copyBtn";
            this.copyBtn.Size = new System.Drawing.Size(75, 23);
            this.copyBtn.TabIndex = 11;
            this.copyBtn.Text = "Copy";
            this.copyBtn.UseVisualStyleBackColor = true;
            this.copyBtn.Click += new System.EventHandler(this.copyBtn_Click);
            // 
            // pasteBtn
            // 
            this.pasteBtn.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.pasteBtn.Location = new System.Drawing.Point(107, 505);
            this.pasteBtn.Name = "pasteBtn";
            this.pasteBtn.Size = new System.Drawing.Size(75, 23);
            this.pasteBtn.TabIndex = 12;
            this.pasteBtn.Text = "Paste";
            this.pasteBtn.UseVisualStyleBackColor = true;
            this.pasteBtn.Click += new System.EventHandler(this.pasteBtn_Click);
            // 
            // playBtn
            // 
            this.playBtn.Location = new System.Drawing.Point(12, 114);
            this.playBtn.Name = "playBtn";
            this.playBtn.Size = new System.Drawing.Size(75, 23);
            this.playBtn.TabIndex = 13;
            this.playBtn.Text = "Play";
            this.playBtn.UseVisualStyleBackColor = true;
            this.playBtn.Click += new System.EventHandler(this.playBtn_Click);
            // 
            // invFourierBtn
            // 
            this.invFourierBtn.Location = new System.Drawing.Point(95, 42);
            this.invFourierBtn.Name = "invFourierBtn";
            this.invFourierBtn.Size = new System.Drawing.Size(75, 23);
            this.invFourierBtn.TabIndex = 14;
            this.invFourierBtn.Text = "InvFourier";
            this.invFourierBtn.UseVisualStyleBackColor = true;
            this.invFourierBtn.Click += new System.EventHandler(this.invFourierBtn_Click);
            // 
            // radioRect
            // 
            this.radioRect.AutoSize = true;
            this.radioRect.Checked = true;
            this.radioRect.Location = new System.Drawing.Point(6, 19);
            this.radioRect.Name = "radioRect";
            this.radioRect.Size = new System.Drawing.Size(83, 17);
            this.radioRect.TabIndex = 15;
            this.radioRect.TabStop = true;
            this.radioRect.Text = "Rectangular";
            this.radioRect.UseVisualStyleBackColor = true;
            // 
            // triRadio
            // 
            this.triRadio.AutoSize = true;
            this.triRadio.Location = new System.Drawing.Point(6, 42);
            this.triRadio.Name = "triRadio";
            this.triRadio.Size = new System.Drawing.Size(72, 17);
            this.triRadio.TabIndex = 16;
            this.triRadio.TabStop = true;
            this.triRadio.Text = "Triangular";
            this.triRadio.UseVisualStyleBackColor = true;
            // 
            // hamRadio
            // 
            this.hamRadio.AutoSize = true;
            this.hamRadio.Location = new System.Drawing.Point(6, 65);
            this.hamRadio.Name = "hamRadio";
            this.hamRadio.Size = new System.Drawing.Size(69, 17);
            this.hamRadio.TabIndex = 17;
            this.hamRadio.TabStop = true;
            this.hamRadio.Text = "Hamming";
            this.hamRadio.UseVisualStyleBackColor = true;
            // 
            // filterBtn
            // 
            this.filterBtn.Location = new System.Drawing.Point(95, 16);
            this.filterBtn.Name = "filterBtn";
            this.filterBtn.Size = new System.Drawing.Size(75, 23);
            this.filterBtn.TabIndex = 19;
            this.filterBtn.Text = "Filter";
            this.filterBtn.UseVisualStyleBackColor = true;
            this.filterBtn.Click += new System.EventHandler(this.filterBtn_Click);
            // 
            // filterValue
            // 
            this.filterValue.Location = new System.Drawing.Point(6, 65);
            this.filterValue.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.filterValue.Name = "filterValue";
            this.filterValue.Size = new System.Drawing.Size(73, 20);
            this.filterValue.TabIndex = 20;
            // 
            // hpRadio
            // 
            this.hpRadio.AutoSize = true;
            this.hpRadio.Location = new System.Drawing.Point(6, 42);
            this.hpRadio.Name = "hpRadio";
            this.hpRadio.Size = new System.Drawing.Size(73, 17);
            this.hpRadio.TabIndex = 21;
            this.hpRadio.TabStop = true;
            this.hpRadio.Text = "High Pass";
            this.hpRadio.UseVisualStyleBackColor = true;
            // 
            // lpRadio
            // 
            this.lpRadio.AutoSize = true;
            this.lpRadio.Checked = true;
            this.lpRadio.Location = new System.Drawing.Point(6, 19);
            this.lpRadio.Name = "lpRadio";
            this.lpRadio.Size = new System.Drawing.Size(71, 17);
            this.lpRadio.TabIndex = 22;
            this.lpRadio.TabStop = true;
            this.lpRadio.Text = "Low Pass";
            this.lpRadio.UseVisualStyleBackColor = true;
            // 
            // filterBox
            // 
            this.filterBox.Controls.Add(this.thrdFilterBtn);
            this.filterBox.Controls.Add(this.aplWinCheck);
            this.filterBox.Controls.Add(this.lpRadio);
            this.filterBox.Controls.Add(this.filterBtn);
            this.filterBox.Controls.Add(this.filterValue);
            this.filterBox.Controls.Add(this.hpRadio);
            this.filterBox.Location = new System.Drawing.Point(12, 174);
            this.filterBox.Name = "filterBox";
            this.filterBox.Size = new System.Drawing.Size(180, 100);
            this.filterBox.TabIndex = 23;
            this.filterBox.TabStop = false;
            this.filterBox.Text = "Filtering";
            // 
            // aplWinCheck
            // 
            this.aplWinCheck.AutoSize = true;
            this.aplWinCheck.Location = new System.Drawing.Point(80, 43);
            this.aplWinCheck.Name = "aplWinCheck";
            this.aplWinCheck.Size = new System.Drawing.Size(94, 17);
            this.aplWinCheck.TabIndex = 23;
            this.aplWinCheck.Text = "Apply Window";
            this.aplWinCheck.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.thrdFourierBtn);
            this.groupBox1.Controls.Add(this.radioRect);
            this.groupBox1.Controls.Add(this.triRadio);
            this.groupBox1.Controls.Add(this.invFourierBtn);
            this.groupBox1.Controls.Add(this.hamRadio);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(12, 280);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(180, 100);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Windowing";
            // 
            // thrdFourierBtn
            // 
            this.thrdFourierBtn.Location = new System.Drawing.Point(95, 71);
            this.thrdFourierBtn.Name = "thrdFourierBtn";
            this.thrdFourierBtn.Size = new System.Drawing.Size(75, 23);
            this.thrdFourierBtn.TabIndex = 25;
            this.thrdFourierBtn.Text = "ThrdFourier";
            this.thrdFourierBtn.UseVisualStyleBackColor = true;
            this.thrdFourierBtn.Click += new System.EventHandler(this.thrdFourierBtn_Click);
            // 
            // thrdFilterBtn
            // 
            this.thrdFilterBtn.Location = new System.Drawing.Point(95, 65);
            this.thrdFilterBtn.Name = "thrdFilterBtn";
            this.thrdFilterBtn.Size = new System.Drawing.Size(75, 23);
            this.thrdFilterBtn.TabIndex = 24;
            this.thrdFilterBtn.Text = "ThrdFilter";
            this.thrdFilterBtn.UseVisualStyleBackColor = true;
            this.thrdFilterBtn.Click += new System.EventHandler(this.thrdFilterBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 959);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.filterBox);
            this.Controls.Add(this.playBtn);
            this.Controls.Add(this.pasteBtn);
            this.Controls.Add(this.copyBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.deleteBtn);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.recordBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.openBtn);
            this.Controls.Add(this.samplChart);
            this.Controls.Add(this.freqChart);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.freqChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.samplChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterValue)).EndInit();
            this.filterBox.ResumeLayout(false);
            this.filterBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart freqChart;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataVisualization.Charting.Chart samplChart;
        private System.Windows.Forms.Button openBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button recordBtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button deleteBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button copyBtn;
        private System.Windows.Forms.Button pasteBtn;
        private System.Windows.Forms.Button playBtn;
        private System.Windows.Forms.Button invFourierBtn;
        private System.Windows.Forms.RadioButton radioRect;
        private System.Windows.Forms.RadioButton triRadio;
        private System.Windows.Forms.RadioButton hamRadio;
        private System.Windows.Forms.Button filterBtn;
        private System.Windows.Forms.NumericUpDown filterValue;
        private System.Windows.Forms.RadioButton hpRadio;
        private System.Windows.Forms.RadioButton lpRadio;
        private System.Windows.Forms.GroupBox filterBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox aplWinCheck;
        private System.Windows.Forms.Button thrdFourierBtn;
        private System.Windows.Forms.Button thrdFilterBtn;
    }
}


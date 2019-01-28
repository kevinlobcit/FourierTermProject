namespace WindowsFormsApplication1
{
    partial class RecordDialog
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
            this.recordBtn = new System.Windows.Forms.Button();
            this.endRecordBtn = new System.Windows.Forms.Button();
            this.playBtn = new System.Windows.Forms.Button();
            this.endPlayBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // recordBtn
            // 
            this.recordBtn.Location = new System.Drawing.Point(54, 72);
            this.recordBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.recordBtn.Name = "recordBtn";
            this.recordBtn.Size = new System.Drawing.Size(210, 74);
            this.recordBtn.TabIndex = 0;
            this.recordBtn.Text = "Record";
            this.recordBtn.UseVisualStyleBackColor = true;
            this.recordBtn.Click += new System.EventHandler(this.recordBtn_Click);
            // 
            // endRecordBtn
            // 
            this.endRecordBtn.Location = new System.Drawing.Point(360, 75);
            this.endRecordBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.endRecordBtn.Name = "endRecordBtn";
            this.endRecordBtn.Size = new System.Drawing.Size(194, 69);
            this.endRecordBtn.TabIndex = 1;
            this.endRecordBtn.Text = "End";
            this.endRecordBtn.UseVisualStyleBackColor = true;
            this.endRecordBtn.Click += new System.EventHandler(this.endRecordBtn_Click);
            // 
            // playBtn
            // 
            this.playBtn.Location = new System.Drawing.Point(54, 208);
            this.playBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.playBtn.Name = "playBtn";
            this.playBtn.Size = new System.Drawing.Size(216, 62);
            this.playBtn.TabIndex = 2;
            this.playBtn.Text = "playBtn";
            this.playBtn.UseVisualStyleBackColor = true;
            // 
            // endPlayBtn
            // 
            this.endPlayBtn.Location = new System.Drawing.Point(363, 200);
            this.endPlayBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.endPlayBtn.Name = "endPlayBtn";
            this.endPlayBtn.Size = new System.Drawing.Size(189, 68);
            this.endPlayBtn.TabIndex = 3;
            this.endPlayBtn.Text = "End";
            this.endPlayBtn.UseVisualStyleBackColor = true;
            // 
            // RecordDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 365);
            this.Controls.Add(this.endPlayBtn);
            this.Controls.Add(this.playBtn);
            this.Controls.Add(this.endRecordBtn);
            this.Controls.Add(this.recordBtn);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "RecordDialog";
            this.Text = "Record";
            this.Load += new System.EventHandler(this.RecordDialog_Load);
            this.ResumeLayout(false);

        }

        //private void recordBtn_Click(object sender, System.EventArgs e)
        //{
        //    throw new System.NotImplementedException();
        //}

        #endregion

        private System.Windows.Forms.Button recordBtn;
        private System.Windows.Forms.Button endRecordBtn;
        private System.Windows.Forms.Button playBtn;
        private System.Windows.Forms.Button endPlayBtn;
    }
}
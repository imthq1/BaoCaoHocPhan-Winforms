namespace GUI.GUI.User_HocSinh
{
    partial class DapAn
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
            this.groupbox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbDapAnDiem = new System.Windows.Forms.Label();
            this.lbDapAnSai = new System.Windows.Forms.Label();
            this.lbDapAnDung = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // groupbox
            // 
            this.groupbox.Location = new System.Drawing.Point(19, 10);
            this.groupbox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupbox.Name = "groupbox";
            this.groupbox.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupbox.Size = new System.Drawing.Size(919, 451);
            this.groupbox.TabIndex = 0;
            this.groupbox.TabStop = false;
            this.groupbox.Text = "Đáp Án";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 471);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Đáp án đúng :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 496);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Đáp án sai:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 520);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Điểm";
            // 
            // lbDapAnDiem
            // 
            this.lbDapAnDiem.AutoSize = true;
            this.lbDapAnDiem.Location = new System.Drawing.Point(97, 520);
            this.lbDapAnDiem.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbDapAnDiem.Name = "lbDapAnDiem";
            this.lbDapAnDiem.Size = new System.Drawing.Size(31, 13);
            this.lbDapAnDiem.TabIndex = 6;
            this.lbDapAnDiem.Text = "Điểm";
            // 
            // lbDapAnSai
            // 
            this.lbDapAnSai.AutoSize = true;
            this.lbDapAnSai.Location = new System.Drawing.Point(97, 496);
            this.lbDapAnSai.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbDapAnSai.Name = "lbDapAnSai";
            this.lbDapAnSai.Size = new System.Drawing.Size(61, 13);
            this.lbDapAnSai.TabIndex = 5;
            this.lbDapAnSai.Text = "Đáp án sai:";
            // 
            // lbDapAnDung
            // 
            this.lbDapAnDung.AutoSize = true;
            this.lbDapAnDung.Location = new System.Drawing.Point(97, 471);
            this.lbDapAnDung.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbDapAnDung.Name = "lbDapAnDung";
            this.lbDapAnDung.Size = new System.Drawing.Size(76, 13);
            this.lbDapAnDung.TabIndex = 4;
            this.lbDapAnDung.Text = "Đáp án đúng :";
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(809, 481);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 42);
            this.button1.TabIndex = 7;
            this.button1.Text = "Thoát";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DapAn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 547);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbDapAnDiem);
            this.Controls.Add(this.lbDapAnSai);
            this.Controls.Add(this.lbDapAnDung);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DapAn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quizz";
            this.Load += new System.EventHandler(this.DapAn_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbDapAnDiem;
        private System.Windows.Forms.Label lbDapAnSai;
        private System.Windows.Forms.Label lbDapAnDung;
        private System.Windows.Forms.Button button1;
    }
}
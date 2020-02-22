namespace SymbolRecognizer
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1_Scale = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button2_Analyze = new System.Windows.Forms.Button();
            this.button3_Clean = new System.Windows.Forms.Button();
            this.button6_TESTSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(250, 337);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button1_Scale
            // 
            this.button1_Scale.Location = new System.Drawing.Point(268, 156);
            this.button1_Scale.Name = "button1_Scale";
            this.button1_Scale.Size = new System.Drawing.Size(100, 50);
            this.button1_Scale.TabIndex = 1;
            this.button1_Scale.Text = "Apply scaling";
            this.button1_Scale.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(374, 13);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(250, 337);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // button2_Analyze
            // 
            this.button2_Analyze.Enabled = false;
            this.button2_Analyze.Location = new System.Drawing.Point(374, 356);
            this.button2_Analyze.Name = "button2_Analyze";
            this.button2_Analyze.Size = new System.Drawing.Size(249, 50);
            this.button2_Analyze.TabIndex = 3;
            this.button2_Analyze.Text = "Analyze";
            this.button2_Analyze.UseVisualStyleBackColor = true;
            // 
            // button3_Clean
            // 
            this.button3_Clean.Location = new System.Drawing.Point(12, 356);
            this.button3_Clean.Name = "button3_Clean";
            this.button3_Clean.Size = new System.Drawing.Size(250, 50);
            this.button3_Clean.TabIndex = 6;
            this.button3_Clean.Text = "Clean";
            this.button3_Clean.UseVisualStyleBackColor = true;
            // 
            // button6_TESTSave
            // 
            this.button6_TESTSave.Location = new System.Drawing.Point(374, 412);
            this.button6_TESTSave.Name = "button6_TESTSave";
            this.button6_TESTSave.Size = new System.Drawing.Size(250, 50);
            this.button6_TESTSave.TabIndex = 9;
            this.button6_TESTSave.Text = "Save";
            this.button6_TESTSave.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 470);
            this.Controls.Add(this.button6_TESTSave);
            this.Controls.Add(this.button3_Clean);
            this.Controls.Add(this.button2_Analyze);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.button1_Scale);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1_Scale;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button2_Analyze;
        private System.Windows.Forms.Button button3_Clean;
        private System.Windows.Forms.Button button6_TESTSave;
    }
}


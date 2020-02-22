namespace SymbolRecognizer
{
    partial class Form2
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
            this.label1 = new System.Windows.Forms.Label();
            this.button2_WrongSymbol = new System.Windows.Forms.Button();
            this.button1_Approve = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1_DataOfCompare = new System.Windows.Forms.TextBox();
            this.button3_EnterText = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(9, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "Data of compare";
            // 
            // button2_WrongSymbol
            // 
            this.button2_WrongSymbol.Enabled = false;
            this.button2_WrongSymbol.Location = new System.Drawing.Point(205, 361);
            this.button2_WrongSymbol.Name = "button2_WrongSymbol";
            this.button2_WrongSymbol.Size = new System.Drawing.Size(110, 50);
            this.button2_WrongSymbol.TabIndex = 14;
            this.button2_WrongSymbol.Text = "Wrong symbol!";
            this.button2_WrongSymbol.UseVisualStyleBackColor = true;
            // 
            // button1_Approve
            // 
            this.button1_Approve.Enabled = false;
            this.button1_Approve.Location = new System.Drawing.Point(89, 361);
            this.button1_Approve.Name = "button1_Approve";
            this.button1_Approve.Size = new System.Drawing.Size(110, 50);
            this.button1_Approve.TabIndex = 13;
            this.button1_Approve.Text = "Approve";
            this.button1_Approve.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(12, 327);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(227, 20);
            this.textBox2.TabIndex = 12;
            // 
            // textBox1_DataOfCompare
            // 
            this.textBox1_DataOfCompare.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox1_DataOfCompare.Enabled = false;
            this.textBox1_DataOfCompare.Location = new System.Drawing.Point(12, 31);
            this.textBox1_DataOfCompare.Multiline = true;
            this.textBox1_DataOfCompare.Name = "textBox1_DataOfCompare";
            this.textBox1_DataOfCompare.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1_DataOfCompare.Size = new System.Drawing.Size(303, 290);
            this.textBox1_DataOfCompare.TabIndex = 11;
            // 
            // button3_EnterText
            // 
            this.button3_EnterText.Enabled = false;
            this.button3_EnterText.Location = new System.Drawing.Point(245, 327);
            this.button3_EnterText.Name = "button3_EnterText";
            this.button3_EnterText.Size = new System.Drawing.Size(70, 23);
            this.button3_EnterText.TabIndex = 16;
            this.button3_EnterText.Text = "Enter";
            this.button3_EnterText.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 423);
            this.Controls.Add(this.button3_EnterText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2_WrongSymbol);
            this.Controls.Add(this.button1_Approve);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1_DataOfCompare);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2_WrongSymbol;
        private System.Windows.Forms.Button button1_Approve;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1_DataOfCompare;
        private System.Windows.Forms.Button button3_EnterText;
    }
}
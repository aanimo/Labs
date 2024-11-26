namespace SimplexMetodGUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            constTextBox = new TextBox();
            label2 = new Label();
            varTextBox = new TextBox();
            button1 = new Button();
            panel1 = new Panel();
            resultLabel = new Label();
            button2 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(417, 72);
            label1.Name = "label1";
            label1.Size = new Size(182, 25);
            label1.TabIndex = 4;
            label1.Text = "Кол-во ограничений";
            // 
            // constTextBox
            // 
            constTextBox.Location = new Point(610, 69);
            constTextBox.Name = "constTextBox";
            constTextBox.Size = new Size(49, 31);
            constTextBox.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(417, 23);
            label2.Name = "label2";
            label2.Size = new Size(177, 25);
            label2.TabIndex = 6;
            label2.Text = "Кол-во переменных";
            // 
            // varTextBox
            // 
            varTextBox.Location = new Point(610, 20);
            varTextBox.Name = "varTextBox";
            varTextBox.Size = new Size(49, 31);
            varTextBox.TabIndex = 5;
            // 
            // button1
            // 
            button1.Location = new Point(701, 74);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 8;
            button1.Text = "Применить";
            button1.TextAlign = ContentAlignment.MiddleRight;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panel1
            // 
            panel1.Location = new Point(37, 114);
            panel1.Name = "panel1";
            panel1.Size = new Size(1084, 355);
            panel1.TabIndex = 9;
            // 
            // resultLabel
            // 
            resultLabel.AutoSize = true;
            resultLabel.Location = new Point(37, 512);
            resultLabel.Name = "resultLabel";
            resultLabel.Size = new Size(182, 25);
            resultLabel.TabIndex = 10;
            resultLabel.Text = "Кол-во ограничений";
            // 
            // button2
            // 
            button2.Location = new Point(37, 475);
            button2.Name = "button2";
            button2.Size = new Size(87, 34);
            button2.TabIndex = 11;
            button2.Text = "Решить";
            button2.TextAlign = ContentAlignment.MiddleRight;
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1174, 637);
            Controls.Add(button2);
            Controls.Add(resultLabel);
            Controls.Add(panel1);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(varTextBox);
            Controls.Add(label1);
            Controls.Add(constTextBox);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox constTextBox;
        private Label label2;
        private TextBox varTextBox;
        private Button button1;
        private Panel panel1;
        private Label resultLabel;
        private Button button2;
    }
}

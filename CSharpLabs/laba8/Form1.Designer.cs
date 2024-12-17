namespace laba8
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
            this.dataGridViewStudents = new System.Windows.Forms.DataGridView();
            this.recordBookTextBox = new System.Windows.Forms.TextBox();
            this.fullNameTextBox = new System.Windows.Forms.TextBox();
            this.groupTextBox = new System.Windows.Forms.TextBox();
            this.departmentComboBox = new System.Windows.Forms.ComboBox();
            this.specificationComboBox = new System.Windows.Forms.ComboBox();
            this.dateOfAdmissionPicker = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudents)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewStudents
            // 
            this.dataGridViewStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStudents.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewStudents.Name = "dataGridViewStudents";
            this.dataGridViewStudents.RowHeadersWidth = 62;
            this.dataGridViewStudents.RowTemplate.Height = 28;
            this.dataGridViewStudents.Size = new System.Drawing.Size(776, 261);
            this.dataGridViewStudents.TabIndex = 0;
            // 
            // recordBookTextBox
            // 
            this.recordBookTextBox.Location = new System.Drawing.Point(71, 321);
            this.recordBookTextBox.Name = "recordBookTextBox";
            this.recordBookTextBox.Size = new System.Drawing.Size(100, 26);
            this.recordBookTextBox.TabIndex = 1;
            // 
            // fullNameTextBox
            // 
            this.fullNameTextBox.Location = new System.Drawing.Point(71, 377);
            this.fullNameTextBox.Name = "fullNameTextBox";
            this.fullNameTextBox.Size = new System.Drawing.Size(100, 26);
            this.fullNameTextBox.TabIndex = 2;
            // 
            // groupTextBox
            // 
            this.groupTextBox.Location = new System.Drawing.Point(431, 368);
            this.groupTextBox.Name = "groupTextBox";
            this.groupTextBox.Size = new System.Drawing.Size(100, 26);
            this.groupTextBox.TabIndex = 3;
            // 
            // departmentComboBox
            // 
            this.departmentComboBox.FormattingEnabled = true;
            this.departmentComboBox.Location = new System.Drawing.Point(226, 321);
            this.departmentComboBox.Name = "departmentComboBox";
            this.departmentComboBox.Size = new System.Drawing.Size(121, 28);
            this.departmentComboBox.TabIndex = 4;
            this.departmentComboBox.SelectedIndexChanged += new System.EventHandler(this.DepartmentComboBox_SelectedIndexChanged);
            // 
            // specificationComboBox
            // 
            this.specificationComboBox.FormattingEnabled = true;
            this.specificationComboBox.Location = new System.Drawing.Point(226, 377);
            this.specificationComboBox.Name = "specificationComboBox";
            this.specificationComboBox.Size = new System.Drawing.Size(121, 28);
            this.specificationComboBox.TabIndex = 5;
            // 
            // dateOfAdmissionPicker
            // 
            this.dateOfAdmissionPicker.Location = new System.Drawing.Point(431, 321);
            this.dateOfAdmissionPicker.Name = "dateOfAdmissionPicker";
            this.dateOfAdmissionPicker.Size = new System.Drawing.Size(200, 26);
            this.dateOfAdmissionPicker.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(662, 279);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 34);
            this.button1.TabIndex = 7;
            this.button1.Text = "Добавить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.AddStudent);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(662, 321);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(126, 38);
            this.button2.TabIndex = 8;
            this.button2.Text = "Обновить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.UpdateStudent);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(662, 365);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(126, 29);
            this.button3.TabIndex = 9;
            this.button3.Text = "Удалить";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.DeleteStudent);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dateOfAdmissionPicker);
            this.Controls.Add(this.specificationComboBox);
            this.Controls.Add(this.departmentComboBox);
            this.Controls.Add(this.groupTextBox);
            this.Controls.Add(this.fullNameTextBox);
            this.Controls.Add(this.recordBookTextBox);
            this.Controls.Add(this.dataGridViewStudents);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudents)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewStudents;
        private System.Windows.Forms.TextBox recordBookTextBox;
        private System.Windows.Forms.TextBox fullNameTextBox;
        private System.Windows.Forms.TextBox groupTextBox;
        private System.Windows.Forms.ComboBox departmentComboBox;
        private System.Windows.Forms.ComboBox specificationComboBox;
        private System.Windows.Forms.DateTimePicker dateOfAdmissionPicker;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}


namespace Monitoring
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblCPULoad = new System.Windows.Forms.Label();
            this.lblCPUFrequency = new System.Windows.Forms.Label();
            this.lblCPUCores = new System.Windows.Forms.Label();
            this.lblCPUName = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblRAMFree = new System.Windows.Forms.Label();
            this.lblRAMUsed = new System.Windows.Forms.Label();
            this.lblRAMTotal = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lblDiskInfo = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.lblGPUMemory = new System.Windows.Forms.Label();
            this.lblGPUName = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.lvProcesses = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pbCpu = new System.Windows.Forms.ProgressBar();
            this.pbRam = new System.Windows.Forms.ProgressBar();
            this.pbDisk = new System.Windows.Forms.ProgressBar();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1194, 419);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pbCpu);
            this.tabPage1.Controls.Add(this.lblCPULoad);
            this.tabPage1.Controls.Add(this.lblCPUFrequency);
            this.tabPage1.Controls.Add(this.lblCPUCores);
            this.tabPage1.Controls.Add(this.lblCPUName);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1186, 386);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Процессор";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblCPULoad
            // 
            this.lblCPULoad.AutoSize = true;
            this.lblCPULoad.Location = new System.Drawing.Point(30, 162);
            this.lblCPULoad.Name = "lblCPULoad";
            this.lblCPULoad.Size = new System.Drawing.Size(51, 20);
            this.lblCPULoad.TabIndex = 3;
            this.lblCPULoad.Text = "label1";
            // 
            // lblCPUFrequency
            // 
            this.lblCPUFrequency.AutoSize = true;
            this.lblCPUFrequency.Location = new System.Drawing.Point(30, 118);
            this.lblCPUFrequency.Name = "lblCPUFrequency";
            this.lblCPUFrequency.Size = new System.Drawing.Size(51, 20);
            this.lblCPUFrequency.TabIndex = 2;
            this.lblCPUFrequency.Text = "label1";
            // 
            // lblCPUCores
            // 
            this.lblCPUCores.AutoSize = true;
            this.lblCPUCores.Location = new System.Drawing.Point(30, 73);
            this.lblCPUCores.Name = "lblCPUCores";
            this.lblCPUCores.Size = new System.Drawing.Size(51, 20);
            this.lblCPUCores.TabIndex = 1;
            this.lblCPUCores.Text = "label1";
            // 
            // lblCPUName
            // 
            this.lblCPUName.AutoSize = true;
            this.lblCPUName.Location = new System.Drawing.Point(30, 35);
            this.lblCPUName.Name = "lblCPUName";
            this.lblCPUName.Size = new System.Drawing.Size(51, 20);
            this.lblCPUName.TabIndex = 0;
            this.lblCPUName.Text = "label1";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.pbRam);
            this.tabPage2.Controls.Add(this.lblRAMFree);
            this.tabPage2.Controls.Add(this.lblRAMUsed);
            this.tabPage2.Controls.Add(this.lblRAMTotal);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1186, 386);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Оперативная память";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblRAMFree
            // 
            this.lblRAMFree.AutoSize = true;
            this.lblRAMFree.Location = new System.Drawing.Point(36, 105);
            this.lblRAMFree.Name = "lblRAMFree";
            this.lblRAMFree.Size = new System.Drawing.Size(51, 20);
            this.lblRAMFree.TabIndex = 2;
            this.lblRAMFree.Text = "label3";
            // 
            // lblRAMUsed
            // 
            this.lblRAMUsed.AutoSize = true;
            this.lblRAMUsed.Location = new System.Drawing.Point(36, 69);
            this.lblRAMUsed.Name = "lblRAMUsed";
            this.lblRAMUsed.Size = new System.Drawing.Size(51, 20);
            this.lblRAMUsed.TabIndex = 1;
            this.lblRAMUsed.Text = "label2";
            // 
            // lblRAMTotal
            // 
            this.lblRAMTotal.AutoSize = true;
            this.lblRAMTotal.Location = new System.Drawing.Point(36, 32);
            this.lblRAMTotal.Name = "lblRAMTotal";
            this.lblRAMTotal.Size = new System.Drawing.Size(51, 20);
            this.lblRAMTotal.TabIndex = 0;
            this.lblRAMTotal.Text = "label1";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.pbDisk);
            this.tabPage3.Controls.Add(this.lblDiskInfo);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1186, 386);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Диски";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lblDiskInfo
            // 
            this.lblDiskInfo.AutoSize = true;
            this.lblDiskInfo.Location = new System.Drawing.Point(102, 41);
            this.lblDiskInfo.Name = "lblDiskInfo";
            this.lblDiskInfo.Size = new System.Drawing.Size(51, 20);
            this.lblDiskInfo.TabIndex = 0;
            this.lblDiskInfo.Text = "label1";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.lblGPUMemory);
            this.tabPage4.Controls.Add(this.lblGPUName);
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1186, 386);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Графика";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // lblGPUMemory
            // 
            this.lblGPUMemory.AutoSize = true;
            this.lblGPUMemory.Location = new System.Drawing.Point(35, 81);
            this.lblGPUMemory.Name = "lblGPUMemory";
            this.lblGPUMemory.Size = new System.Drawing.Size(51, 20);
            this.lblGPUMemory.TabIndex = 1;
            this.lblGPUMemory.Text = "label2";
            // 
            // lblGPUName
            // 
            this.lblGPUName.AutoSize = true;
            this.lblGPUName.Location = new System.Drawing.Point(35, 33);
            this.lblGPUName.Name = "lblGPUName";
            this.lblGPUName.Size = new System.Drawing.Size(51, 20);
            this.lblGPUName.TabIndex = 0;
            this.lblGPUName.Text = "label1";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.lvProcesses);
            this.tabPage5.Location = new System.Drawing.Point(4, 29);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(1186, 386);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Процессы";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // lvProcesses
            // 
            this.lvProcesses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvProcesses.HideSelection = false;
            this.lvProcesses.Location = new System.Drawing.Point(28, 71);
            this.lvProcesses.Name = "lvProcesses";
            this.lvProcesses.Size = new System.Drawing.Size(1103, 312);
            this.lvProcesses.TabIndex = 0;
            this.lvProcesses.UseCompatibleStateImageBehavior = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Имя процесса";
            // 
            // pbCpu
            // 
            this.pbCpu.Location = new System.Drawing.Point(44, 223);
            this.pbCpu.Name = "pbCpu";
            this.pbCpu.Size = new System.Drawing.Size(100, 23);
            this.pbCpu.TabIndex = 4;
            // 
            // pbRam
            // 
            this.pbRam.Location = new System.Drawing.Point(40, 185);
            this.pbRam.Name = "pbRam";
            this.pbRam.Size = new System.Drawing.Size(100, 23);
            this.pbRam.TabIndex = 5;
            // 
            // pbDisk
            // 
            this.pbDisk.Location = new System.Drawing.Point(106, 181);
            this.pbDisk.Name = "pbDisk";
            this.pbDisk.Size = new System.Drawing.Size(100, 23);
            this.pbDisk.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1218, 570);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label lblCPULoad;
        private System.Windows.Forms.Label lblCPUFrequency;
        private System.Windows.Forms.Label lblCPUCores;
        private System.Windows.Forms.Label lblCPUName;
        private System.Windows.Forms.Label lblRAMFree;
        private System.Windows.Forms.Label lblRAMUsed;
        private System.Windows.Forms.Label lblRAMTotal;
        private System.Windows.Forms.Label lblDiskInfo;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label lblGPUMemory;
        private System.Windows.Forms.Label lblGPUName;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.ListView lvProcesses;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ProgressBar pbCpu;
        private System.Windows.Forms.ProgressBar pbRam;
        private System.Windows.Forms.ProgressBar pbDisk;
    }
}


namespace PCMonitoringApp
{
    partial class MainForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.processesTabPage = new System.Windows.Forms.TabPage();
            this.monitoringTab = new System.Windows.Forms.TabPage();
            this.ramUsageLabel = new System.Windows.Forms.Label();
            this.cpuUsageLabel = new System.Windows.Forms.Label();
            this.ramChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cpuChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.hardwareTab = new System.Windows.Forms.TabPage();
            this.gpuInfoLabel = new System.Windows.Forms.Label();
            this.hddInfoLabel = new System.Windows.Forms.Label();
            this.ramInfoLabel = new System.Windows.Forms.Label();
            this.cpuInfoLabel = new System.Windows.Forms.Label();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.logsTabPage = new System.Windows.Forms.TabPage();
            this.loggingGroupBox = new System.Windows.Forms.GroupBox();
            this.selectedFileLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.browseButton = new System.Windows.Forms.Button();
            this.logRamCheckBox = new System.Windows.Forms.CheckBox();
            this.logCpuCheckBox = new System.Windows.Forms.CheckBox();
            this.monitoringTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ramChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpuChart)).BeginInit();
            this.hardwareTab.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.logsTabPage.SuspendLayout();
            this.loggingGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // processesTabPage
            // 
            this.processesTabPage.Location = new System.Drawing.Point(4, 29);
            this.processesTabPage.Name = "processesTabPage";
            this.processesTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.processesTabPage.Size = new System.Drawing.Size(1116, 488);
            this.processesTabPage.TabIndex = 2;
            this.processesTabPage.Text = "Список процессов";
            this.processesTabPage.UseVisualStyleBackColor = true;
            // 
            // monitoringTab
            // 
            this.monitoringTab.Controls.Add(this.ramUsageLabel);
            this.monitoringTab.Controls.Add(this.cpuUsageLabel);
            this.monitoringTab.Controls.Add(this.ramChart);
            this.monitoringTab.Controls.Add(this.cpuChart);
            this.monitoringTab.Location = new System.Drawing.Point(4, 29);
            this.monitoringTab.Name = "monitoringTab";
            this.monitoringTab.Padding = new System.Windows.Forms.Padding(3);
            this.monitoringTab.Size = new System.Drawing.Size(1116, 488);
            this.monitoringTab.TabIndex = 1;
            this.monitoringTab.Text = "Мониторинг системы";
            this.monitoringTab.UseVisualStyleBackColor = true;
            // 
            // ramUsageLabel
            // 
            this.ramUsageLabel.AutoSize = true;
            this.ramUsageLabel.Location = new System.Drawing.Point(660, 412);
            this.ramUsageLabel.Name = "ramUsageLabel";
            this.ramUsageLabel.Size = new System.Drawing.Size(51, 20);
            this.ramUsageLabel.TabIndex = 3;
            this.ramUsageLabel.Text = "label2";
            // 
            // cpuUsageLabel
            // 
            this.cpuUsageLabel.AutoSize = true;
            this.cpuUsageLabel.Location = new System.Drawing.Point(66, 393);
            this.cpuUsageLabel.Name = "cpuUsageLabel";
            this.cpuUsageLabel.Size = new System.Drawing.Size(51, 20);
            this.cpuUsageLabel.TabIndex = 2;
            this.cpuUsageLabel.Text = "label1";
            // 
            // ramChart
            // 
            chartArea1.Name = "ChartArea1";
            this.ramChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.ramChart.Legends.Add(legend1);
            this.ramChart.Location = new System.Drawing.Point(594, 6);
            this.ramChart.Name = "ramChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.ramChart.Series.Add(series1);
            this.ramChart.Size = new System.Drawing.Size(516, 376);
            this.ramChart.TabIndex = 1;
            this.ramChart.Text = "chart2";
            // 
            // cpuChart
            // 
            chartArea2.Name = "ChartArea1";
            this.cpuChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.cpuChart.Legends.Add(legend2);
            this.cpuChart.Location = new System.Drawing.Point(6, 6);
            this.cpuChart.Name = "cpuChart";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.cpuChart.Series.Add(series2);
            this.cpuChart.Size = new System.Drawing.Size(547, 367);
            this.cpuChart.TabIndex = 0;
            this.cpuChart.Text = "chart1";
            // 
            // hardwareTab
            // 
            this.hardwareTab.Controls.Add(this.gpuInfoLabel);
            this.hardwareTab.Controls.Add(this.hddInfoLabel);
            this.hardwareTab.Controls.Add(this.ramInfoLabel);
            this.hardwareTab.Controls.Add(this.cpuInfoLabel);
            this.hardwareTab.Location = new System.Drawing.Point(4, 29);
            this.hardwareTab.Name = "hardwareTab";
            this.hardwareTab.Padding = new System.Windows.Forms.Padding(3);
            this.hardwareTab.Size = new System.Drawing.Size(1116, 488);
            this.hardwareTab.TabIndex = 0;
            this.hardwareTab.Text = "Аппаратная часть";
            this.hardwareTab.UseVisualStyleBackColor = true;
            // 
            // gpuInfoLabel
            // 
            this.gpuInfoLabel.AutoSize = true;
            this.gpuInfoLabel.Location = new System.Drawing.Point(56, 167);
            this.gpuInfoLabel.Name = "gpuInfoLabel";
            this.gpuInfoLabel.Size = new System.Drawing.Size(51, 20);
            this.gpuInfoLabel.TabIndex = 3;
            this.gpuInfoLabel.Text = "label4";
            // 
            // hddInfoLabel
            // 
            this.hddInfoLabel.AutoSize = true;
            this.hddInfoLabel.Location = new System.Drawing.Point(56, 123);
            this.hddInfoLabel.Name = "hddInfoLabel";
            this.hddInfoLabel.Size = new System.Drawing.Size(51, 20);
            this.hddInfoLabel.TabIndex = 2;
            this.hddInfoLabel.Text = "label3";
            // 
            // ramInfoLabel
            // 
            this.ramInfoLabel.AutoSize = true;
            this.ramInfoLabel.Location = new System.Drawing.Point(56, 74);
            this.ramInfoLabel.Name = "ramInfoLabel";
            this.ramInfoLabel.Size = new System.Drawing.Size(51, 20);
            this.ramInfoLabel.TabIndex = 1;
            this.ramInfoLabel.Text = "label2";
            // 
            // cpuInfoLabel
            // 
            this.cpuInfoLabel.AutoSize = true;
            this.cpuInfoLabel.Location = new System.Drawing.Point(56, 30);
            this.cpuInfoLabel.Name = "cpuInfoLabel";
            this.cpuInfoLabel.Size = new System.Drawing.Size(51, 20);
            this.cpuInfoLabel.TabIndex = 0;
            this.cpuInfoLabel.Text = "label1";
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.hardwareTab);
            this.mainTabControl.Controls.Add(this.monitoringTab);
            this.mainTabControl.Controls.Add(this.processesTabPage);
            this.mainTabControl.Controls.Add(this.logsTabPage);
            this.mainTabControl.Location = new System.Drawing.Point(12, 12);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(1124, 521);
            this.mainTabControl.TabIndex = 0;
            // 
            // logsTabPage
            // 
            this.logsTabPage.Controls.Add(this.loggingGroupBox);
            this.logsTabPage.Location = new System.Drawing.Point(4, 29);
            this.logsTabPage.Name = "logsTabPage";
            this.logsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.logsTabPage.Size = new System.Drawing.Size(1116, 488);
            this.logsTabPage.TabIndex = 3;
            this.logsTabPage.Text = "Настройки";
            this.logsTabPage.UseVisualStyleBackColor = true;
            // 
            // loggingGroupBox
            // 
            this.loggingGroupBox.Controls.Add(this.selectedFileLabel);
            this.loggingGroupBox.Controls.Add(this.label1);
            this.loggingGroupBox.Controls.Add(this.browseButton);
            this.loggingGroupBox.Controls.Add(this.logRamCheckBox);
            this.loggingGroupBox.Controls.Add(this.logCpuCheckBox);
            this.loggingGroupBox.Location = new System.Drawing.Point(265, 26);
            this.loggingGroupBox.Name = "loggingGroupBox";
            this.loggingGroupBox.Size = new System.Drawing.Size(577, 244);
            this.loggingGroupBox.TabIndex = 0;
            this.loggingGroupBox.TabStop = false;
            this.loggingGroupBox.Text = "Логгирование";
            // 
            // selectedFileLabel
            // 
            this.selectedFileLabel.AutoSize = true;
            this.selectedFileLabel.Location = new System.Drawing.Point(272, 182);
            this.selectedFileLabel.Name = "selectedFileLabel";
            this.selectedFileLabel.Size = new System.Drawing.Size(51, 20);
            this.selectedFileLabel.TabIndex = 5;
            this.selectedFileLabel.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 182);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(248, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Файл для записи логирования:";
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(18, 130);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(173, 29);
            this.browseButton.TabIndex = 3;
            this.browseButton.Text = "Выбрать файл";
            this.browseButton.UseVisualStyleBackColor = true;
            // 
            // logRamCheckBox
            // 
            this.logRamCheckBox.AutoSize = true;
            this.logRamCheckBox.Location = new System.Drawing.Point(18, 90);
            this.logRamCheckBox.Name = "logRamCheckBox";
            this.logRamCheckBox.Size = new System.Drawing.Size(102, 24);
            this.logRamCheckBox.TabIndex = 1;
            this.logRamCheckBox.Text = "RAM Log";
            this.logRamCheckBox.UseVisualStyleBackColor = true;
            // 
            // logCpuCheckBox
            // 
            this.logCpuCheckBox.AutoSize = true;
            this.logCpuCheckBox.Location = new System.Drawing.Point(18, 59);
            this.logCpuCheckBox.Name = "logCpuCheckBox";
            this.logCpuCheckBox.Size = new System.Drawing.Size(99, 24);
            this.logCpuCheckBox.TabIndex = 0;
            this.logCpuCheckBox.Text = "CPU Log";
            this.logCpuCheckBox.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 545);
            this.Controls.Add(this.mainTabControl);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.monitoringTab.ResumeLayout(false);
            this.monitoringTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ramChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpuChart)).EndInit();
            this.hardwareTab.ResumeLayout(false);
            this.hardwareTab.PerformLayout();
            this.mainTabControl.ResumeLayout(false);
            this.logsTabPage.ResumeLayout(false);
            this.loggingGroupBox.ResumeLayout(false);
            this.loggingGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage processesTabPage;
        private System.Windows.Forms.TabPage monitoringTab;
        private System.Windows.Forms.Label ramUsageLabel;
        private System.Windows.Forms.Label cpuUsageLabel;
        private System.Windows.Forms.DataVisualization.Charting.Chart ramChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart cpuChart;
        private System.Windows.Forms.TabPage hardwareTab;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.Label gpuInfoLabel;
        private System.Windows.Forms.Label hddInfoLabel;
        private System.Windows.Forms.Label ramInfoLabel;
        private System.Windows.Forms.Label cpuInfoLabel;
        private System.Windows.Forms.TabPage logsTabPage;
        private System.Windows.Forms.GroupBox loggingGroupBox;
        private System.Windows.Forms.CheckBox logRamCheckBox;
        private System.Windows.Forms.CheckBox logCpuCheckBox;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Label selectedFileLabel;
        private System.Windows.Forms.Label label1;
    }
}


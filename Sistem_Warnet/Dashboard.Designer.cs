namespace Sistem_Warnet
{
    partial class Dashboard_Form
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
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartPendapatan = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblTotalPendapatan = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chartPendapatan)).BeginInit();
            this.SuspendLayout();
            // 
            // chartPendapatan
            // 
            chartArea1.Name = "ChartArea1";
            this.chartPendapatan.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartPendapatan.Legends.Add(legend1);
            this.chartPendapatan.Location = new System.Drawing.Point(18, 45);
            this.chartPendapatan.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chartPendapatan.Name = "chartPendapatan";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartPendapatan.Series.Add(series1);
            this.chartPendapatan.Size = new System.Drawing.Size(996, 561);
            this.chartPendapatan.TabIndex = 0;
            this.chartPendapatan.Text = "chart1";
            // 
            // lblTotalPendapatan
            // 
            this.lblTotalPendapatan.AutoSize = true;
            this.lblTotalPendapatan.Location = new System.Drawing.Point(14, 629);
            this.lblTotalPendapatan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalPendapatan.Name = "lblTotalPendapatan";
            this.lblTotalPendapatan.Size = new System.Drawing.Size(232, 20);
            this.lblTotalPendapatan.TabIndex = 1;
            this.lblTotalPendapatan.Text = "Total Pendapatan Keseluruhan:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(440, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "PENDAPATAN CHART";
            // 
            // Dashboard_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTotalPendapatan);
            this.Controls.Add(this.chartPendapatan);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Dashboard_Form";
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.Dashboard_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartPendapatan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartPendapatan;
        private System.Windows.Forms.Label lblTotalPendapatan;
        private System.Windows.Forms.Label label1;
    }
}
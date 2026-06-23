namespace Sistem_Warnet
{
    partial class FormRekapPenghasilan
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.labelMulai = new System.Windows.Forms.Label();
            this.dtpMulai = new System.Windows.Forms.DateTimePicker();
            this.labelSelesai = new System.Windows.Forms.Label();
            this.dtpSelesai = new System.Windows.Forms.DateTimePicker();
            this.btnFilter = new System.Windows.Forms.Button();
            this.dgvRekap = new System.Windows.Forms.DataGridView();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.lblTotalTransaksi = new System.Windows.Forms.Label();
            this.lblTotalPendapatan = new System.Windows.Forms.Label();
            this.btnCetak = new System.Windows.Forms.Button();
            this.btnKembali = new System.Windows.Forms.Button();
            this.wbPrint = new System.Windows.Forms.WebBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRekap)).BeginInit();
            this.pnlSummary.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(165)))), ((int)(((byte)(250)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(325, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "REKAP PENGHASILAN WARNET";
            // 
            // labelMulai
            // 
            this.labelMulai.AutoSize = true;
            this.labelMulai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.labelMulai.Location = new System.Drawing.Point(20, 75);
            this.labelMulai.Name = "labelMulai";
            this.labelMulai.Size = new System.Drawing.Size(95, 20);
            this.labelMulai.TabIndex = 1;
            this.labelMulai.Text = "Dari Tanggal:";
            // 
            // dtpMulai
            // 
            this.dtpMulai.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpMulai.Location = new System.Drawing.Point(120, 71);
            this.dtpMulai.Name = "dtpMulai";
            this.dtpMulai.Size = new System.Drawing.Size(130, 27);
            this.dtpMulai.TabIndex = 2;
            // 
            // labelSelesai
            // 
            this.labelSelesai.AutoSize = true;
            this.labelSelesai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.labelSelesai.Location = new System.Drawing.Point(280, 75);
            this.labelSelesai.Name = "labelSelesai";
            this.labelSelesai.Size = new System.Drawing.Size(117, 20);
            this.labelSelesai.TabIndex = 3;
            this.labelSelesai.Text = "Sampai Tanggal:";
            // 
            // dtpSelesai
            // 
            this.dtpSelesai.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpSelesai.Location = new System.Drawing.Point(400, 71);
            this.dtpSelesai.Name = "dtpSelesai";
            this.dtpSelesai.Size = new System.Drawing.Size(130, 27);
            this.dtpSelesai.TabIndex = 4;
            // 
            // btnFilter
            // 
            this.btnFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.btnFilter.FlatAppearance.BorderSize = 0;
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnFilter.ForeColor = System.Drawing.Color.White;
            this.btnFilter.Location = new System.Drawing.Point(560, 68);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(140, 32);
            this.btnFilter.TabIndex = 5;
            this.btnFilter.Text = "Tampilkan Data";
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // dgvRekap
            // 
            this.dgvRekap.AllowUserToAddRows = false;
            this.dgvRekap.AllowUserToDeleteRows = false;
            this.dgvRekap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRekap.Location = new System.Drawing.Point(20, 120);
            this.dgvRekap.Name = "dgvRekap";
            this.dgvRekap.ReadOnly = true;
            this.dgvRekap.Size = new System.Drawing.Size(860, 350);
            this.dgvRekap.TabIndex = 6;
            // 
            // pnlSummary
            // 
            this.pnlSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlSummary.Controls.Add(this.lblTotalTransaksi);
            this.pnlSummary.Controls.Add(this.lblTotalPendapatan);
            this.pnlSummary.Location = new System.Drawing.Point(20, 490);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new System.Drawing.Size(500, 50);
            this.pnlSummary.TabIndex = 7;
            // 
            // lblTotalTransaksi
            // 
            this.lblTotalTransaksi.AutoSize = true;
            this.lblTotalTransaksi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalTransaksi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblTotalTransaksi.Location = new System.Drawing.Point(15, 15);
            this.lblTotalTransaksi.Name = "lblTotalTransaksi";
            this.lblTotalTransaksi.Size = new System.Drawing.Size(127, 19);
            this.lblTotalTransaksi.TabIndex = 0;
            this.lblTotalTransaksi.Text = "Total Transaksi: 0";
            // 
            // lblTotalPendapatan
            // 
            this.lblTotalPendapatan.AutoSize = true;
            this.lblTotalPendapatan.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotalPendapatan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblTotalPendapatan.Location = new System.Drawing.Point(220, 15);
            this.lblTotalPendapatan.Name = "lblTotalPendapatan";
            this.lblTotalPendapatan.Size = new System.Drawing.Size(200, 20);
            this.lblTotalPendapatan.TabIndex = 1;
            this.lblTotalPendapatan.Text = "Total Pendapatan: Rp 0";
            // 
            // btnCetak
            // 
            this.btnCetak.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnCetak.FlatAppearance.BorderSize = 0;
            this.btnCetak.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCetak.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCetak.ForeColor = System.Drawing.Color.White;
            this.btnCetak.Location = new System.Drawing.Point(590, 490);
            this.btnCetak.Name = "btnCetak";
            this.btnCetak.Size = new System.Drawing.Size(140, 50);
            this.btnCetak.TabIndex = 8;
            this.btnCetak.Text = "Cetak Rekap";
            this.btnCetak.UseVisualStyleBackColor = false;
            this.btnCetak.Click += new System.EventHandler(this.btnCetak_Click);
            // 
            // btnKembali
            // 
            this.btnKembali.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnKembali.FlatAppearance.BorderSize = 0;
            this.btnKembali.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKembali.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnKembali.ForeColor = System.Drawing.Color.White;
            this.btnKembali.Location = new System.Drawing.Point(740, 490);
            this.btnKembali.Name = "btnKembali";
            this.btnKembali.Size = new System.Drawing.Size(140, 50);
            this.btnKembali.TabIndex = 9;
            this.btnKembali.Text = "Kembali";
            this.btnKembali.UseVisualStyleBackColor = false;
            this.btnKembali.Click += new System.EventHandler(this.btnKembali_Click);
            // 
            // wbPrint
            // 
            this.wbPrint.Location = new System.Drawing.Point(825, 20);
            this.wbPrint.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbPrint.Name = "wbPrint";
            this.wbPrint.Size = new System.Drawing.Size(55, 25);
            this.wbPrint.TabIndex = 10;
            this.wbPrint.Visible = false;
            // 
            // FormRekapPenghasilan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.ClientSize = new System.Drawing.Size(900, 562);
            this.Controls.Add(this.wbPrint);
            this.Controls.Add(this.btnKembali);
            this.Controls.Add(this.btnCetak);
            this.Controls.Add(this.pnlSummary);
            this.Controls.Add(this.dgvRekap);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.dtpSelesai);
            this.Controls.Add(this.labelSelesai);
            this.Controls.Add(this.dtpMulai);
            this.Controls.Add(this.labelMulai);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormRekapPenghasilan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rekap Penghasilan";
            this.Load += new System.EventHandler(this.FormRekapPenghasilan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRekap)).EndInit();
            this.pnlSummary.ResumeLayout(false);
            this.pnlSummary.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label labelMulai;
        private System.Windows.Forms.DateTimePicker dtpMulai;
        private System.Windows.Forms.Label labelSelesai;
        private System.Windows.Forms.DateTimePicker dtpSelesai;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.DataGridView dgvRekap;
        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.Label lblTotalTransaksi;
        private System.Windows.Forms.Label lblTotalPendapatan;
        private System.Windows.Forms.Button btnCetak;
        private System.Windows.Forms.Button btnKembali;
        private System.Windows.Forms.WebBrowser wbPrint;
    }
}

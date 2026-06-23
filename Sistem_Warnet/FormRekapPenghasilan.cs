using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sistem_Warnet
{
    public partial class FormRekapPenghasilan : Form
    {
        private DAL dbLogic = new DAL();
        private DataTable dtRekap;
        private int totalPendapatan = 0;
        private int totalTransaksi = 0;

        public FormRekapPenghasilan()
        {
            InitializeComponent();
            
            // Bind the WebBrowser event for printing
            wbPrint.DocumentCompleted += WbPrint_DocumentCompleted;
        }

        private void FormRekapPenghasilan_Load(object sender, EventArgs e)
        {
            UIHelper.ApplyTheme(this);

            // Set default date range to the current month (from 1st day of month to today)
            DateTime today = DateTime.Today;
            dtpMulai.Value = new DateTime(today.Year, today.Month, 1);
            dtpSelesai.Value = today;

            // Load initial data
            LoadData();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                DateTime mulai = dtpMulai.Value;
                DateTime selesai = dtpSelesai.Value;

                if (mulai > selesai)
                {
                    MessageBox.Show("Tanggal mulai tidak boleh melebihi tanggal selesai!", "Validasi Tanggal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Query transaction recap data
                dtRekap = dbLogic.GetRekapTransaksi(mulai, selesai);
                dgvRekap.DataSource = dtRekap;

                // Configure grid header formatting immediately
                UIHelper.ApplyTheme(this);

                // Calculate summary aggregates
                totalTransaksi = dtRekap.Rows.Count;
                totalPendapatan = 0;

                foreach (DataRow row in dtRekap.Rows)
                {
                    totalPendapatan += Convert.ToInt32(row["Total Bayar"]);
                }

                // Update UI Labels
                lblTotalTransaksi.Text = "Total Transaksi: " + totalTransaksi;
                lblTotalPendapatan.Text = "Total Pendapatan: Rp " + totalPendapatan.ToString("N0");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat rekap penghasilan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCetak_Click(object sender, EventArgs e)
        {
            if (dtRekap == null || dtRekap.Rows.Count == 0)
            {
                MessageBox.Show("Tidak ada data rekap untuk dicetak!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // 1. Gandakan dtRekap untuk disesuaikan namanya dengan DataSet
                DataTable dtSource = dtRekap.Copy();
                dtSource.TableName = "RekapTransaksi";

                // 2. Load berkas Crystal Report yang baru dibuat
                ReportRekap rpt = new ReportRekap();
                rpt.SetDataSource(dtSource);

                // 3. Tampilkan Crystal Report Viewer secara langsung
                // (Kita buat form viewer baru secara runtime agar praktis)
                Form printPreviewForm = new Form();
                printPreviewForm.Text = "Cetak Rekap Laporan";
                printPreviewForm.Size = new Size(1000, 700);
                printPreviewForm.StartPosition = FormStartPosition.CenterScreen;

                CrystalDecisions.Windows.Forms.CrystalReportViewer viewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
                viewer.Dock = DockStyle.Fill;
                viewer.ActiveViewIndex = -1;
                viewer.BorderStyle = BorderStyle.FixedSingle;
                viewer.ShowGroupTreeButton = false;
                viewer.ShowParameterPanelButton = false;

                viewer.ReportSource = rpt;
                printPreviewForm.Controls.Add(viewer);

                // Tampilkan jendela print preview
                printPreviewForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mencetak rekap: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void WbPrint_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            // Trigger the print dialog when document finishes loading
            wbPrint.ShowPrintDialog();
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

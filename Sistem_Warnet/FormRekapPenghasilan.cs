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
                // Generate receipt HTML template
                StringBuilder html = new StringBuilder();
                html.Append("<!DOCTYPE html><html><head><meta charset='utf-8'><style>");
                html.Append("body { font-family: 'Segoe UI', Arial, sans-serif; margin: 30px; color: #334155; line-height: 1.5; }");
                html.Append("h1 { text-align: center; color: #1E3A8A; margin-bottom: 4px; font-size: 24px; text-transform: uppercase; letter-spacing: 1px; }");
                html.Append("h3 { text-align: center; color: #64748B; font-weight: normal; margin-top: 0; margin-bottom: 30px; font-size: 14px; }");
                html.Append(".summary-container { display: table; width: 100%; border: 1px solid #CBD5E1; border-radius: 8px; margin-bottom: 25px; border-collapse: separate; border-spacing: 0; }");
                html.Append(".summary-cell { display: table-cell; padding: 16px; width: 50%; vertical-align: middle; }");
                html.Append(".summary-cell:first-child { border-right: 1px solid #CBD5E1; }");
                html.Append(".summary-label { font-size: 12px; color: #64748B; text-transform: uppercase; letter-spacing: 0.5px; margin-bottom: 4px; }");
                html.Append(".summary-value { font-size: 22px; font-weight: bold; color: #0F172A; }");
                html.Append(".summary-value.income { color: #059669; }");
                html.Append("table { width: 100%; border-collapse: collapse; margin-top: 10px; }");
                html.Append("th { background-color: #0F172A; color: #FFFFFF; padding: 12px 10px; text-align: left; font-size: 12px; text-transform: uppercase; letter-spacing: 0.5px; }");
                html.Append("td { padding: 10px; border-bottom: 1px solid #E2E8F0; font-size: 13px; color: #334155; }");
                html.Append("tr:nth-child(even) { background-color: #F8FAFC; }");
                html.Append(".footer { text-align: center; margin-top: 40px; font-size: 11px; color: #94A3B8; border-top: 1px solid #E2E8F0; padding-top: 15px; }");
                html.Append("</style></head><body>");

                html.Append("<h1>LAPORAN REKAP PENGHASILAN</h1>");
                html.Append($"<h3>Periode Laporan: {dtpMulai.Value.ToString("dd MMM yyyy")} s/d {dtpSelesai.Value.ToString("dd MMM yyyy")}</h3>");

                // Summary aggregates cards
                html.Append("<div class='summary-container'>");
                html.Append("<div class='summary-cell'>");
                html.Append("<div class='summary-label'>Total Transaksi</div>");
                html.Append($"<div class='summary-value'>{totalTransaksi}</div>");
                html.Append("</div>");
                html.Append("<div class='summary-cell'>");
                html.Append("<div class='summary-label'>Total Pendapatan</div>");
                html.Append($"<div class='summary-value income'>Rp {totalPendapatan.ToString("N0")}</div>");
                html.Append("</div>");
                html.Append("</div>");

                // Data Table
                html.Append("<table>");
                html.Append("<thead><tr><th>Tanggal Transaksi</th><th>Tier PC</th><th>Durasi</th><th>Total Bayar</th><th>Operator</th></tr></thead>");
                html.Append("<tbody>");

                foreach (DataRow row in dtRekap.Rows)
                {
                    html.Append("<tr>");
                    html.Append($"<td>{Convert.ToDateTime(row["Tanggal Transaksi"]).ToString("dd/MM/yyyy HH:mm")}</td>");
                    html.Append($"<td>{row["Tier PC"]}</td>");
                    html.Append($"<td>{row["Durasi Jam"]} Jam</td>");
                    html.Append($"<td>Rp {Convert.ToInt32(row["Total Bayar"]).ToString("N0")}</td>");
                    html.Append($"<td>{row["Operator"]}</td>");
                    html.Append("</tr>");
                }

                html.Append("</tbody></table>");
                
                // Footer
                html.Append($"<div class='footer'>Sistem Rental Warnet &copy; {DateTime.Now.Year} &bull; Laporan dicetak pada: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}</div>");
                html.Append("</body></html>");

                // Load HTML into the invisible browser to trigger printing
                wbPrint.DocumentText = html.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyiapkan dokumen cetak: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

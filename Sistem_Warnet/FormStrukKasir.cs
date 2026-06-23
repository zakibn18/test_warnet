using System;
using System.Data;
using System.Windows.Forms;

namespace Sistem_Warnet
{
    public partial class FormStrukKasir : Form
    {
        private string kodeVoucher;
        DAL dbLogic = new DAL();

        // Komponen WebBrowser untuk memproses cetakan HTML
        private WebBrowser wbCetak = new WebBrowser();

        public FormStrukKasir(string kode)
        {
            InitializeComponent();
            this.kodeVoucher = kode;

            // Daftarkan event setelah HTML selesai dimuat di memori
            wbCetak.DocumentCompleted += WbCetak_DocumentCompleted;
        }

        private void FormStrukKasir_Load(object sender, EventArgs e)
        {
            UIHelper.ApplyTheme(this);
            try
            {
                // 1. Tarik data dari Database
                DataTable dt = dbLogic.CetakStrukKasir(kodeVoucher);

                // Kotak Detektor Database: Jika ini muncul "0", baru kita salahkan databasenya
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Database tidak mengirimkan data! Cek apakah transaksi tersimpan.", "Error");
                    return;
                }

                // 2. TRIK AMPUH: Ganti nama DataTable agar dikira sebagai Object C# oleh Crystal Reports
                // Nama ini diambil persis dari teks yang ada di Field Explorer Anda!
                dt.TableName = "Sistem_Warnet_DSWarnet";

                // 3. Masukkan data ke file .rpt Anda yang baru
                ReportStruk rpt = new ReportStruk();
                rpt.SetDataSource(dt);

                // 4. Tampilkan ke layar
                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat struk: " + ex.Message);
            }
        }

        private void WbCetak_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            // 4. KUNCI UTAMA: Jalankan perintah cetak otomatis setelah HTML selesai dirender
            // Gunakan wbCetak.ShowPrintDialog() jika ingin memunculkan pilihan printer
            // Gunakan wbCetak.Print() jika ingin langsung mencetak ke printer default tanpa pop-up
            wbCetak.ShowPrintDialog();

            // Tutup form struk otomatis setelah dialog cetak selesai ditangani operator
            this.Close();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void crystalReportViewer1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistem_Warnet
{
    public partial class ClientDashboard : Form
    {
        private DAL dbLogic = new DAL();
        private string currentVoucher;
        private int sisaDetik;

        // Constructor wajib menerima 3 parameter ini dari Login_Form
        public ClientDashboard(string voucher, string nomorPc, int detikAwal)
        {
            InitializeComponent();
            this.currentVoucher = voucher;
            this.sisaDetik = detikAwal;

            lblInfoPC.Text = "Login di: " + nomorPc;
        }

        private void ClientDashboard_Load(object sender, EventArgs e)
        {
            UIHelper.ApplyTheme(this);

            // Make the sisa waktu text stand out and style labels
            lblSisaWaktu.Font = new Font(UIHelper.FontName, 26f, FontStyle.Bold);
            lblSisaWaktu.ForeColor = UIHelper.ColorTextAccent;
            lblInfoPC.Font = new Font(UIHelper.FontName, 18f, FontStyle.Regular);
            lblInfoPC.ForeColor = UIHelper.ColorTextSecondary;

            // Menghilangkan tombol Close (X) di pojok kanan atas agar klien tidak bisa kabur tanpa prosedur
            this.ControlBox = false;

            UpdateLabelWaktu();

            // Konfigurasi Timer (1000 milidetik = 1 detik)
            timer1.Interval = 1000;

            // Mengikat fungsi timer1_Tick agar berjalan setiap detik
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sisaDetik--; // Kurangi waktu 1 detik

            if (sisaDetik <= 0)
            {
                // Eksekusi jika waktu habis
                timer1.Stop();
                UpdateLabelWaktu();

                // Panggil fungsi pemutus akses di database
                dbLogic.LogoutClient(currentVoucher);

                MessageBox.Show("Waktu Anda telah habis. Sesi ditutup otomatis.", "Waktu Habis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                KembaliKeLogin();
            }
            else
            {
                UpdateLabelWaktu();
            }
        }

        private void UpdateLabelWaktu()
        {
            // Kalkulasi matematika memecah total detik menjadi format Jam dan Menit
            int jam = sisaDetik / 3600;
            int menit = (sisaDetik % 3600) / 60;

            // Format D2 memastikan angka tunggal memiliki 0 di depan (contoh: angka 5 menjadi "05")
            lblSisaWaktu.Text = $"{jam:D2}:{menit:D2}";
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Sisa waktu Anda akan tetap berjalan di latar belakang. Yakin ingin Logout sekarang?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialog == DialogResult.Yes)
            {
                timer1.Stop();

                /* * ATURAN BISNIS WARNET:
                 * Jika ingin sisa waktu HANGUS (tidak bisa dipakai lagi) saat klien klik Logout, 
                 * hapus tanda komentar pada baris di bawah ini:
                 */
                // dbLogic.LogoutClient(currentVoucher);

                KembaliKeLogin();
            }
        }

        private void KembaliKeLogin()
        {
            // Memanggil kembali layar utama setelah klien keluar
            Login_Form login = new Login_Form();
            login.Show();
            this.Close();
        }
        private void lblSisaWaktu_Click(object sender, EventArgs e)
        {

        }

        private void lblInfoPC_Click(object sender, EventArgs e)
        {

        }
    }
}

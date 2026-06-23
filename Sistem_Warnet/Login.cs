using System;
using System.Data; // Wajib ditambahkan untuk menggunakan DataRow
using System.Windows.Forms;

namespace Sistem_Warnet
{
    public partial class Login_Form : Form
    {
        private DAL dbLogic = new DAL();

        public Login_Form()
        {
            InitializeComponent();
            txtPassword.UseSystemPasswordChar = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string inputUsername = txtUsername.Text.Trim();
                string inputPassword = txtPassword.Text.Trim();

                // 1. Cek apakah ini login Admin atau Operator
                Staff loggedInUser = dbLogic.CekLogin(inputUsername, inputPassword);

                if (loggedInUser != null)
                {
                    this.Hide();
                    if (loggedInUser.Role == "Admin")
                    {
                        new Warnet_Form().Show();
                    }
                    else
                    {
                        new Operator_Form(loggedInUser).Show();
                    }
                    return; // Hentikan eksekusi di sini jika berhasil login sebagai staff
                }

                // 2. Jika bukan staff, cek apakah input di kolom Username adalah Kode Voucher Pelanggan
                DataRow sessionData = dbLogic.LoginClient(inputUsername);

                if (sessionData != null)
                {
                    int sisaDetik = Convert.ToInt32(sessionData["sisa_detik_aktual"]);
                    string nomorPc = sessionData["nomor_pc"].ToString();

                    if (sisaDetik <= 0)
                    {
                        MessageBox.Show("Waktu voucher Anda sudah habis!", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dbLogic.LogoutClient(inputUsername); // Tutup sesi otomatis di database
                    }
                    else
                    {
                        this.Hide();
                        // Buka Dashboard Client
                        ClientDashboard dashboard = new ClientDashboard(inputUsername, nomorPc, sisaDetik);
                        dashboard.Show();
                    }
                }
                else
                {
                    // 3. Jika bukan staff dan bukan voucher yang valid
                    MessageBox.Show("Login Gagal! Username/Password salah, atau Kode Voucher tidak ditemukan/habis.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Koneksi gagal: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Koneksi berhasil.");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UIHelper.ApplyTheme(this);
        }
        private void label1_Click(object sender, EventArgs e) { }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using ExcelDataReader;


namespace Sistem_Warnet
{
    public partial class Warnet_Form : Form
    {
        // 1. Panggil class DAL, hapus SqlConnection mentah
        private DAL dbLogic = new DAL();
        private BindingSource bindingSource = new BindingSource();

        public Warnet_Form()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = dbLogic.GetAllMasterPC();
                bindingSource.DataSource = dt;
                dataGridView1.DataSource = bindingSource;
                bindingNavigator1.BindingSource = bindingSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal Load: " + ex.Message);
            }
        }

        private void LoadTierToComboBox()
        {
            try
            {
                DataTable dt = dbLogic.GetTierComboBox();
                cmbTier.DataSource = dt;
                cmbTier.DisplayMember = "nama_tier";
                cmbTier.ValueMember = "id_tier";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat Tier: " + ex.Message);
            }
        }

        private void Warnet_Form_Load(object sender, EventArgs e)
        {
            UIHelper.ApplyTheme(this);

            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("Tersedia");
            cmbStatus.Items.Add("Maintenance");

            LoadTierToComboBox();
            LoadData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = dbLogic.SearchMasterPC(txtPencarian.Text);
                bindingSource.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Data PC tidak ditemukan.", "Pencarian", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mencari data: " + ex.Message);
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            string input = txtNoPC.Text.ToUpper().Trim();

            // Validasi Input
            if (string.IsNullOrEmpty(input)) { txtNoPC.Focus(); return; }
            if (!Regex.IsMatch(input, @"^(PC|VIP)-\d+$")) { MessageBox.Show("Format tidak valid!"); txtNoPC.Focus(); return; }

            string tierTerpilih = cmbTier.Text;
            if (input.StartsWith("PC-") && tierTerpilih != "Reguler") { MessageBox.Show("Harus tier Reguler!"); cmbTier.Focus(); return; }
            if (input.StartsWith("VIP-") && tierTerpilih != "VIP") { MessageBox.Show("Harus tier VIP!"); cmbTier.Focus(); return; }

            try
            {
                int idTier = Convert.ToInt32(cmbTier.SelectedValue);
                dbLogic.InsertMasterPC(idTier, input, cmbStatus.Text);

                MessageBox.Show("Data berhasil disimpan!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal Simpan: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) { MessageBox.Show("Pilih data!"); return; }
            if (MessageBox.Show("Yakin ingin mengupdate data?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.No) return;

            string input = txtNoPC.Text.ToUpper().Trim();

            // Validasi Input
            if (string.IsNullOrEmpty(input)) { txtNoPC.Focus(); return; }
            if (!Regex.IsMatch(input, @"^(PC|VIP)-\d+$")) { MessageBox.Show("Format tidak valid!"); txtNoPC.Focus(); return; }

            string tierTerpilih = cmbTier.Text;
            if (input.StartsWith("PC-") && tierTerpilih != "Reguler") { MessageBox.Show("Harus tier Reguler!"); cmbTier.Focus(); return; }
            if (input.StartsWith("VIP-") && tierTerpilih != "VIP") { MessageBox.Show("Harus tier VIP!"); cmbTier.Focus(); return; }

            try
            {
                int idTier = Convert.ToInt32(cmbTier.SelectedValue);
                int idPc = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id_pc"].Value);

                dbLogic.UpdateMasterPC(idTier, input, cmbStatus.Text, idPc);

                MessageBox.Show("Data berhasil diupdate!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal Update: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) { MessageBox.Show("Pilih data!"); return; }

            if (MessageBox.Show("Yakin ingin menghapus data?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    int idPc = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id_pc"].Value);
                    dbLogic.DeleteMasterPC(idPc);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal Hapus: " + ex.Message);
                }
            }
        }

        private void btnTotal_Click(object sender, EventArgs e)
        {
            try
            {
                int total = dbLogic.CountMasterPC();
                lblTotal.Text = "Total PC Terdaftar: " + total;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal Hitung: " + ex.Message);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                dbLogic.ResetMasterPC();
                MessageBox.Show("Data berhasil direset dari tabel backup!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Reset gagal: " + ex.Message);
            }
        }

        private void dgvDataPC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtNoPC.Text = row.Cells["nomor_pc"].Value.ToString();
                cmbStatus.Text = row.Cells["status"].Value.ToString();
            }
        }

        private void cmbTier_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tierTerpilih = cmbTier.Text;
            if (string.IsNullOrEmpty(txtNoPC.Text) || txtNoPC.Text == "PC-" || txtNoPC.Text == "VIP-")
            {
                if (tierTerpilih == "Reguler") txtNoPC.Text = "PC-";
                else if (tierTerpilih == "VIP") txtNoPC.Text = "VIP-";

                txtNoPC.SelectionStart = txtNoPC.Text.Length;
            }
        }

        private void btnConnect_Click(object sender, EventArgs e) { LoadData(); }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            new Login_Form().Show();
            this.Close();
        }

        // Event kosong dibiarkan agar designer tidak error
        private void label2_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void txtNoPC_TextChanged(object sender, EventArgs e) { }
        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e) { }
        private void button1_Click(object sender, EventArgs e) { }
        private void btnTestInjection_Click(object sender, EventArgs e) { }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Dashboard_Form dashboard = new Dashboard_Form();
            // Gunakan ShowDialog agar form utama tidak bisa di-klik selama dashboard terbuka
            dashboard.ShowDialog();
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            // 1. Buka jendela dialog untuk memilih file Excel
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Excel Workbook|*.xlsx|Excel 97-2003|*.xls" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // 2. Baca file Excel yang dipilih menjadi FileStream
                        using (var stream = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read))
                        {
                            // 3. Terjemahkan Stream Excel menggunakan ExcelDataReader
                            using (var reader = ExcelReaderFactory.CreateReader(stream))
                            {
                                // 4. Konversi seluruh isi sheet Excel menjadi bentuk DataTable C#
                                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                                });

                                // Ambil Sheet pertama (index 0)
                                DataTable dtExcel = result.Tables[0];

                                // 5. Kirim DataTable ke DAL untuk dieksekusi ke SQL Server
                                int berhasil = dbLogic.ImportDataPCMassal(dtExcel);

                                // 6. Notifikasi hasil dan segarkan tabel di layar
                                MessageBox.Show(berhasil + " data PC baru berhasil diimpor dari Excel!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData();
                            }
                        }
                    }
                    catch (IOException)
                    {
                        // Error ini spesifik muncul jika file Excel-nya masih terbuka (sedang diedit) di aplikasi Microsoft Excel
                        MessageBox.Show("Gagal memuat! Pastikan file Excel sedang tidak dibuka di aplikasi Microsoft Excel. Tutup file tersebut terlebih dahulu lalu coba lagi.", "Peringatan Akses", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Terjadi kesalahan sistem: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnRekapPenghasilan_Click(object sender, EventArgs e)
        {
            FormRekapPenghasilan rekapForm = new FormRekapPenghasilan();
            rekapForm.ShowDialog();
        }
    }
}

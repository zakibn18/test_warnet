using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistem_Warnet
{
    public partial class Operator_Form : Form
    {
        private DAL dbLogic = new DAL();
        private BindingSource bindingSource = new BindingSource();
        public Staff currentStaff;

        public Operator_Form(Staff user)
        {
            InitializeComponent();
            this.currentStaff = user;
        }

        private void Staff_Form_Load(object sender, EventArgs e)
        {
            UIHelper.ApplyTheme(this);

            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            LoadData();
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
                MessageBox.Show("Gagal memuat data: " + ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = dbLogic.SearchMasterPC(txtPencarian.Text);
                dataGridView1.DataSource = dt; // Operator langsung timpa ke DataSource
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pencarian Gagal: " + ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnPesan_Click(object sender, EventArgs e)
        {
            Transaksi_Form formTransaksi = new Transaksi_Form(this.currentStaff);
            formTransaksi.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            new Login_Form().Show();
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}
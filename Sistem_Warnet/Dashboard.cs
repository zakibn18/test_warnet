using System;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Sistem_Warnet
{
    public partial class Dashboard_Form : Form
    {
        private DAL dbLogic = new DAL();

        public Dashboard_Form()
        {
            InitializeComponent();
        }

        private void Dashboard_Form_Load(object sender, EventArgs e)
        {
            UIHelper.ApplyTheme(this);
            LoadGrafikPendapatan();
        }

        private void LoadGrafikPendapatan()
        {
            try
            {
                DataTable dt = dbLogic.GetStatistikPendapatanTier();

                chartPendapatan.Series.Clear();
                chartPendapatan.Titles.Clear();

                Series series = new Series("Pendapatan");
                series.ChartType = SeriesChartType.Column;
                series.IsValueShownAsLabel = true;
                series.LabelFormat = "N0";

                int totalKeseluruhan = 0;

                foreach (DataRow row in dt.Rows)
                {
                    string namaTier = row["nama_tier"].ToString();
                    int pendapatan = Convert.ToInt32(row["total_pendapatan"]);

                    series.Points.AddXY(namaTier, pendapatan);
                    totalKeseluruhan += pendapatan;
                }

                chartPendapatan.Series.Add(series);
                chartPendapatan.Titles.Add("Statistik Pendapatan Keseluruhan Berdasarkan Tier PC");

                // Style the chart elements again now that the series and titles have been dynamically populated
                UIHelper.ApplyTheme(this);

                lblTotalPendapatan.Text = "Total Pendapatan Keseluruhan: Rp " + totalKeseluruhan.ToString("N0");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat grafik: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
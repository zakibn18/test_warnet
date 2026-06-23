using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistem_Warnet
{
    internal class DAL
    {
        private string connectionString = "Data Source=DESKTOP-8TS9IRD\\ZAKIBN;Initial Catalog=DBWarnet;Integrated Security=True";
        private SqlConnection conn;

        // TAMBAHAN WAJIB: Konstruktor untuk menginisialisasi conn
        public DAL()
        {
            conn = new SqlConnection(connectionString);
        }

        // ==========================================
        // KUMPULAN FUNGSI CRUD UNTUK MASTER PC
        // ==========================================
        public DataTable GetAllMasterPC()
        {
            DataTable dt = new DataTable();
            using (SqlConnection localConn = new SqlConnection(connectionString))
            {
                // Menggunakan view vw_DataPC
                SqlCommand cmd = new SqlCommand("SELECT * FROM vw_DataPC", localConn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable GetTierComboBox()
        {
            DataTable dt = new DataTable();
            using (SqlConnection localConn = new SqlConnection(connectionString))
            {
                // Menggunakan SP sp_GetTierComboBox
                SqlCommand cmd = new SqlCommand("sp_GetTierComboBox", localConn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable SearchMasterPC(string keyword)
        {
            DataTable dt = new DataTable();
            using (SqlConnection localConn = new SqlConnection(connectionString))
            {
                // Menggunakan SP sp_SearchMasterPC
                SqlCommand cmd = new SqlCommand("sp_SearchMasterPC", localConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@search", "%" + keyword + "%");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        public void InsertMasterPC(int idTier, string nomorPc, string status)
        {
            using (SqlConnection localConn = new SqlConnection(connectionString))
            {
                // Menggunakan SP sp_InsertMasterPC
                SqlCommand cmd = new SqlCommand("sp_InsertMasterPC", localConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tier", idTier);
                cmd.Parameters.AddWithValue("@nomor", nomorPc);
                cmd.Parameters.AddWithValue("@status", status);

                localConn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateMasterPC(int idTier, string nomorPc, string status, int idPc)
        {
            using (SqlConnection localConn = new SqlConnection(connectionString))
            {
                // Menggunakan SP sp_UpdateMasterPC
                SqlCommand cmd = new SqlCommand("sp_UpdateMasterPC", localConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tier", idTier);
                cmd.Parameters.AddWithValue("@nomor", nomorPc);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@id", idPc);

                localConn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteMasterPC(int idPc)
        {
            using (SqlConnection localConn = new SqlConnection(connectionString))
            {
                // Menggunakan SP sp_DeleteMasterPC
                SqlCommand cmd = new SqlCommand("sp_DeleteMasterPC", localConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", idPc);

                localConn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public int CountMasterPC()
        {
            using (SqlConnection localConn = new SqlConnection(connectionString))
            {
                // Menggunakan SP sp_CountMasterPC_Output
                SqlCommand cmd = new SqlCommand("sp_CountMasterPC_Output", localConn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter outputParam = new SqlParameter("@Total", SqlDbType.Int);
                outputParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputParam);

                localConn.Open();
                cmd.ExecuteNonQuery();

                return Convert.ToInt32(outputParam.Value);
            }
        }

        public void ResetMasterPC()
        {
            using (SqlConnection localConn = new SqlConnection(connectionString))
            {
                // Query reset dari tabel backup
                string query = @"
            IF OBJECT_ID('dbo.Master_PC_Backup') IS NOT NULL
            BEGIN
                DELETE FROM dbo.Master_PC;
                SET IDENTITY_INSERT dbo.Master_PC ON;
                INSERT INTO dbo.Master_PC (id_pc, id_tier, nomor_pc, status)
                SELECT id_pc, id_tier, nomor_pc, status FROM dbo.Master_PC_Backup;
                SET IDENTITY_INSERT dbo.Master_PC OFF;
            END";
                SqlCommand cmd = new SqlCommand(query, localConn);
                localConn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // ==========================================
        // FUNGSI UNTUK LOGIN & TRANSAKSI KASIR
        // ==========================================

        public Staff CekLogin(string username, string password)
        {
            using (SqlConnection localConn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Login", localConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                localConn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Staff loggedInUser = new Staff();
                    loggedInUser.IdUser = Convert.ToInt32(reader["id_user"]);
                    loggedInUser.Username = reader["username"].ToString();
                    loggedInUser.Role = reader["role"].ToString();
                    return loggedInUser;
                }
                return null; // Mengembalikan null jika kombinasi username/password salah
            }
        }

        public DataTable GetPCTersediaUntukTransaksi()
        {
            DataTable dt = new DataTable();
            using (SqlConnection localConn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT p.id_pc, p.nomor_pc, t.id_tier, t.nama_tier, t.harga_per_jam
            FROM Master_PC p
            INNER JOIN Tier_PC t ON p.id_tier = t.id_tier
            WHERE p.status = 'Tersedia'";

                SqlCommand cmd = new SqlCommand(query, localConn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        public void ResetStatusPCSemuaTersedia()
        {
            using (SqlConnection localConn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Master_PC SET status = 'Tersedia'", localConn);
                localConn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ProsesPembelianVoucher(int idUser, int idTier, int idPc, int durasiJam, int totalBayar, out string kodeVoucherAwal)
        {
            // 1. Generate Kode Voucher Acak (6 Karakter)
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            kodeVoucherAwal = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());

            try
            {
                // 2. Buka Koneksi
                if (conn.State == System.Data.ConnectionState.Closed) conn.Open();

                // 3. Panggil Stored Procedure Transaksi yang baru
                SqlCommand cmd = new SqlCommand("sp_ProsesTransaksiKasir", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // Lempar parameter ke dalam SP
                cmd.Parameters.AddWithValue("@id_user", idUser);
                cmd.Parameters.AddWithValue("@id_tier", idTier);
                cmd.Parameters.AddWithValue("@id_pc", idPc);
                cmd.Parameters.AddWithValue("@durasi_jam", durasiJam);
                cmd.Parameters.AddWithValue("@total_bayar", totalBayar);
                cmd.Parameters.AddWithValue("@kode_voucher", kodeVoucherAwal);

                // Eksekusi
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Tangkap pesan error dari RAISERROR di SQL Server
                throw new Exception("Transaksi Gagal: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();
            }
        }

        public DataTable GetStatistikPendapatanTier()
        {
            DataTable dt = new DataTable();
            using (SqlConnection localConn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_StatistikPendapatanTier", localConn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable CetakStrukKasir(string kodeVoucher)
        {
            if (conn.State == System.Data.ConnectionState.Closed) conn.Open();

            SqlCommand cmd = new SqlCommand("sp_CetakStruk", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@kode_voucher", kodeVoucher);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtStruk = new DataTable();

            // KUNCI UTAMA: Beri nama tabel agar dikenali oleh Crystal Reports
            dtStruk.TableName = "sp_CetakStruk;1";

            da.Fill(dtStruk);

            conn.Close();
            return dtStruk;
        }

        public int ImportDataPCMassal(DataTable dtExcel)
        {
            int jumlahBerhasil = 0;
            using (SqlConnection localConn = new SqlConnection(connectionString))
            {
                localConn.Open();

                // Looping membaca setiap baris data dari Excel
                foreach (DataRow row in dtExcel.Rows)
                {
                    try
                    {
                        // Menarik data berdasarkan nama header kolom Excel
                        string nomor = row["Nomor PC"].ToString().ToUpper().Trim();
                        string tier = row["Tier"].ToString().Trim();
                        string status = row["Status"].ToString().Trim();

                        // Validasi: Lewati jika baris kosong atau format salah
                        if (string.IsNullOrEmpty(nomor)) continue;

                        // Tentukan ID Tier (1 = Reguler, 2 = VIP) sesuai database Anda
                        int idTier = (tier.ToUpper() == "VIP") ? 2 : 1;

                        // Gunakan SP Insert yang sudah ada
                        SqlCommand cmd = new SqlCommand("sp_InsertMasterPC", localConn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@tier", idTier);
                        cmd.Parameters.AddWithValue("@nomor", nomor);
                        cmd.Parameters.AddWithValue("@status", status);

                        cmd.ExecuteNonQuery();
                        jumlahBerhasil++;
                    }
                    catch
                    {
                        // Jika gagal (misal duplikat nomor PC / validasi constraint gagal), 
                        // program akan melewati baris ini dan melanjutkan ke baris berikutnya
                        continue;
                    }
                }
            }
            return jumlahBerhasil;
        }

        public DataRow LoginClient(string kodeVoucher)
        {
            DataTable dt = new DataTable();
            using (SqlConnection localConn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_LoginClient", localConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@kode", kodeVoucher);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            // Kembalikan baris pertama jika ditemukan, kembalikan null jika gagal
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        public void LogoutClient(string kodeVoucher)
        {
            using (SqlConnection localConn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_LogoutClient", localConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@kode", kodeVoucher);

                localConn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
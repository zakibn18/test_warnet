CREATE PROCEDURE sp_ProsesTransaksiKasir
    @id_user INT,
    @id_tier INT,
    @id_pc INT,
    @durasi_jam INT,
    @total_bayar INT,
    @kode_voucher VARCHAR(20)
AS
BEGIN
    -- Matikan pesan ' (1 row affected)' agar performa jaringan jauh lebih cepat
    SET NOCOUNT ON;

    -- MULAI BLOK PENANGKAP ERROR (T-SQL TRY...CATCH)
    BEGIN TRY
        -- 1. KUNCI PINTU TRANSAKSI
        BEGIN TRANSACTION;

        -- Siapkan variabel penampung ID Transaksi yang baru lahir
        DECLARE @new_id_transaksi INT;

        -- [PROSES A] Insert Transaksi Pembelian
        INSERT INTO Transaksi_Pembelian (id_user, id_tier, tgl_transaksi, durasi_jam, total_bayar)
        VALUES (@id_user, @id_tier, GETDATE(), @durasi_jam, @total_bayar);

        -- SCOPE_IDENTITY() menangkap id_transaksi yang baru saja tercipta di baris atas
        SET @new_id_transaksi = SCOPE_IDENTITY();

        -- [PROSES B] Insert Voucher Sesi
        DECLARE @sisa_menit INT = @durasi_jam * 60;
        
        INSERT INTO Voucher_Sesi (kode_voucher, id_transaksi, id_pc, waktu_mulai, sisa_waktu_menit, status_sesi)
        VALUES (@kode_voucher, @new_id_transaksi, @id_pc, GETDATE(), @sisa_menit, 'Aktif');

        -- [PROSES C] Update Status PC
        UPDATE Master_PC 
        SET status = 'Digunakan' 
        WHERE id_pc = @id_pc;

        -- JIKA SAMPAI BARIS INI TIDAK ADA YANG MELEDAK, SAHKAN PERMANEN!
        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH
        -- JIKA ADA ERROR DI TITIK MANAPUN (Misal: kode voucher kembar / tipe data salah)
        -- CEK APAKAH TRANSAKSI MASIH TERBUKA
        IF @@TRANCOUNT > 0
        BEGIN
            -- BATALKAN SELURUH PERUBAHAN! Uang ditarik, PC kembali Tersedia.
            ROLLBACK TRANSACTION;
        END

        -- Tangkap pesan error asli dari SQL Server, lalu lempar ke aplikasi C#
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO


SELECT * FROM Voucher_Sesi







-- Untuk Cetak Struk CrystalReportViewer Operator
CREATE PROCEDURE sp_CetakStruk
    @kode_voucher VARCHAR(6)
AS
BEGIN
    SELECT 
        t.tgl_transaksi, 
        t.durasi_jam, 
        t.total_bayar,
        v.kode_voucher, 
        p.nomor_pc,
        tr.nama_tier,
        u.username AS nama_operator
    FROM Transaksi_Pembelian t
    JOIN Voucher_Sesi v ON t.id_transaksi = v.id_transaksi
    JOIN Master_PC p ON v.id_pc = p.id_pc
    JOIN Tier_PC tr ON t.id_tier = tr.id_tier
    JOIN Pengguna_Staf u ON t.id_user = u.id_user
    WHERE v.kode_voucher = @kode_voucher
END
GO

-- Ada Alter jangan lupa di lihat
USE DBWarnet
-- Untuk melihat Pendapatan per Tier PC
CREATE PROCEDURE sp_StatistikPendapatanTier
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Menggunakan LEFT JOIN agar tier yang belum pernah laku tetap muncul dengan nilai 0
    SELECT 
        t.nama_tier, 
        ISNULL(SUM(tp.total_bayar), 0) AS total_pendapatan,
        COUNT(tp.id_transaksi) AS jumlah_transaksi
    FROM Tier_PC t
    LEFT JOIN Transaksi_Pembelian tp ON t.id_tier = tp.id_tier
    GROUP BY t.nama_tier;
END
GO

ALTER PROCEDURE sp_StatistikPendapatanTier
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        t.nama_tier, 
        ISNULL(SUM(tp.total_bayar), 0) AS total_pendapatan,
        COUNT(tp.id_transaksi) AS jumlah_transaksi
    FROM Tier_PC t WITH (NOLOCK)
    LEFT JOIN Transaksi_Pembelian tp WITH (NOLOCK) ON t.id_tier = tp.id_tier
    GROUP BY t.nama_tier;
END
GO
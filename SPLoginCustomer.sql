-- SP untuk Login Pelanggan
CREATE PROCEDURE sp_LoginClient
    @kode VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    -- Kalkulasi sisa detik dikurangi waktu yang sudah berjalan sejak voucher dicetak
    SELECT 
        v.kode_voucher,
        p.nomor_pc,
        (v.sisa_waktu_menit * 60) - DATEDIFF(SECOND, v.waktu_mulai, GETDATE()) AS sisa_detik_aktual
    FROM Voucher_Sesi v WITH (NOLOCK)
    JOIN Master_PC p WITH (NOLOCK) ON v.id_pc = p.id_pc
    WHERE v.kode_voucher = @kode AND v.status_sesi = 'Aktif';
END
GO

-- SP untuk Logout Pelanggan
CREATE PROCEDURE sp_LogoutClient
    @kode VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    -- 1. Matikan sesi voucher
    UPDATE Voucher_Sesi 
    SET status_sesi = 'Selesai' 
    WHERE kode_voucher = @kode;
    
    -- 2. Otomatis bebaskan PC agar bisa digunakan pelanggan lain
    UPDATE p
    SET p.status = 'Tersedia'
    FROM Master_PC p
    JOIN Voucher_Sesi v ON p.id_pc = v.id_pc
    WHERE v.kode_voucher = @kode;
END
GO
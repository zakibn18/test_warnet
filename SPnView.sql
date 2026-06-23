-- 1. SP untuk LoadData (Tampil PC dengan Nama Tier / JOIN)
CREATE PROCEDURE sp_LoadDataPC
AS
BEGIN
    SET NOCOUNT ON;
    SELECT p.id_pc, p.nomor_pc, t.nama_tier, p.status 
    FROM Master_PC p JOIN Tier_PC t ON p.id_tier = t.id_tier;
END
GO

-- 2. SP untuk LoadTierToComboBox (Tampil Dropdown Tier)
CREATE PROCEDURE sp_GetTierComboBox
AS
BEGIN
    SET NOCOUNT ON;
    SELECT id_tier, nama_tier FROM Tier_PC;
END
GO

-- 3. SP untuk Pencarian Data (Search LIKE)
CREATE PROCEDURE sp_SearchMasterPC
    @search VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM Master_PC WHERE nomor_pc LIKE @search;
END
GO

-- Modifikasi
ALTER PROCEDURE sp_SearchMasterPC
    @search VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    -- Tambahkan JOIN agar nama_tier ikut terpanggil saat dicari
    SELECT p.id_pc, p.nomor_pc, t.nama_tier, p.status 
    FROM Master_PC p 
    JOIN Tier_PC t ON p.id_tier = t.id_tier
    WHERE p.nomor_pc LIKE @search;
END
GO

-- 4. SP untuk Insert Data
CREATE PROCEDURE sp_InsertMasterPC
    @tier INT,
    @nomor VARCHAR(10),
    @status VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Master_PC (id_tier, nomor_pc, status) 
    VALUES (@tier, @nomor, @status);
END
GO

-- 5. SP untuk Update Data
CREATE PROCEDURE sp_UpdateMasterPC
    @tier INT,
    @nomor VARCHAR(10),
    @status VARCHAR(20),
    @id INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Master_PC 
    SET id_tier=@tier, nomor_pc=@nomor, status=@status 
    WHERE id_pc=@id;
END
GO

-- 6. SP untuk Delete Data
CREATE PROCEDURE sp_DeleteMasterPC
    @id INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Master_PC WHERE id_pc = @id;
END
GO

-- 7. SP untuk Tampil Semua Data Basic (btnConnect)
CREATE PROCEDURE sp_GetAllMasterPC
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM Master_PC;
END
GO

-- 8. SP untuk Menghitung Total (Menggunakan OUTPUT Parameter sesuai modul PDF dosen)
CREATE PROCEDURE sp_CountMasterPC_Output
    @Total INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT @Total = COUNT(*) FROM Master_PC;
END
GO

-- SP Untuk Login
CREATE PROCEDURE sp_Login
    @username VARCHAR(50),
    @password VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT id_user, username, role 
    FROM Pengguna_Staf 
    WHERE username = @username AND password = @password;
END

-- Membuat View DataPC
CREATE VIEW vw_DataPC AS
SELECT p.id_pc, p.nomor_pc, t.nama_tier, p.status 
FROM Master_PC p 
JOIN Tier_PC t ON p.id_tier = t.id_tier;

-- SQLi Backup
SELECT * INTO Master_PC_Backup 
FROM Master_PC;
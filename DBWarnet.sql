CREATE DATABASE DBWarnet;
GO

USE DBWarnet;
GO

-- =========================================================
-- 1. PEMBUATAN TABEL MASTER (Parent Tables)
-- =========================================================

-- Membuat Tabel Pengguna/Staf
CREATE TABLE Pengguna_Staf (
    id_user INT IDENTITY(1,1) PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    role VARCHAR(20) NOT NULL CHECK (role IN ('Admin', 'Operator'))
);

-- Membuat Tabel Master Harga (Tier PC)
CREATE TABLE Tier_PC (
    id_tier INT IDENTITY(1,1) PRIMARY KEY,
    nama_tier VARCHAR(50) NOT NULL,
    harga_per_jam INT NOT NULL 
);

-- Membuat Tabel Data Fisik Komputer
CREATE TABLE Master_PC (
    id_pc INT IDENTITY(1,1) PRIMARY KEY,
    id_tier INT NOT NULL,
    nomor_pc VARCHAR(10) NOT NULL UNIQUE,
    status VARCHAR(20) NOT NULL CHECK (status IN ('Tersedia', 'Digunakan', 'Maintenance')),
    
    -- Relasi ke tabel Tier_PC
    FOREIGN KEY (id_tier) REFERENCES Tier_PC(id_tier)
);


-- =========================================================
-- 2. PEMBUATAN TABEL TRANSAKSIONAL (Child Tables)
-- =========================================================

-- Membuat Tabel Transaksi Kasir
CREATE TABLE Transaksi_Pembelian (
    id_transaksi INT IDENTITY(1,1) PRIMARY KEY,
    id_user INT NOT NULL,
    id_tier INT NOT NULL,
    tgl_transaksi DATETIME DEFAULT GETDATE(), 
    durasi_jam INT NOT NULL,
    total_bayar INT NOT NULL,
    
    -- Relasi ke Kasir yang melayani & Tier yang dibeli
    FOREIGN KEY (id_user) REFERENCES Pengguna_Staf(id_user),
    FOREIGN KEY (id_tier) REFERENCES Tier_PC(id_tier)
);

-- Membuat Tabel Voucher / Sesi Bermain
CREATE TABLE Voucher_Sesi (
    kode_voucher VARCHAR(10) PRIMARY KEY,
    id_transaksi INT NOT NULL UNIQUE, -- UNIQUE memastikan relasi One-to-One
    id_pc INT NULL, -- NULL karena saat voucher dicetak, PC belum dipilih
    waktu_mulai DATETIME NULL,
    sisa_waktu_menit INT NOT NULL,
    status_sesi VARCHAR(20) NOT NULL DEFAULT 'Aktif' CHECK (status_sesi IN ('Aktif', 'Selesai')),
    
    -- Relasi ke Struk Transaksi dan PC tempat login
    FOREIGN KEY (id_transaksi) REFERENCES Transaksi_Pembelian(id_transaksi),
    FOREIGN KEY (id_pc) REFERENCES Master_PC(id_pc)
);

--====================================
--				DML
--====================================
-- ID 1 akan menjadi Admin, ID 2 akan menjadi Operator
INSERT INTO Pengguna_Staf (username, password, role) 
VALUES 
('admin_budi', 'hashpassword123', 'Admin'),
('kasir_siti', 'hashpassword456', 'Operator');

-- ID 1 = Reguler, ID 2 = VIP
INSERT INTO Tier_PC (nama_tier, harga_per_jam) 
VALUES 
('Reguler', 5000),
('VIP', 8000);

-- Memasukkan 2 PC Reguler (id_tier = 1) dan 1 VIP (id_tier = 2)
INSERT INTO Master_PC (id_tier, nomor_pc, status) 
VALUES 
(1, 'PC-01', 'Tersedia'),
(1, 'PC-02', 'Tersedia'),
(2, 'VIP-01', 'Tersedia');










-- Baru buat di jalanin

-- 1. Kosongkan tabel yang saling berelasi agar tidak ada data sisa yang melanggar
DELETE FROM Voucher_Sesi;
DELETE FROM Master_PC;
GO

-- 2. Hapus constraint lama jika ada
IF EXISTS (SELECT * FROM sys.objects WHERE name = 'CHK_NomorPC_Format' AND type = 'C')
    ALTER TABLE Master_PC DROP CONSTRAINT CHK_NomorPC_Format;
GO

-- 3. Pasang Pagar Aturan Baru: Wajib PC- atau VIP-, dan TANPA karakter spesial
ALTER TABLE Master_PC
ADD CONSTRAINT CHK_NomorPC_Format 
CHECK (
    (nomor_pc LIKE 'PC-%' OR nomor_pc LIKE 'VIP-%') 
    AND 
    nomor_pc NOT LIKE '%[^a-zA-Z0-9-]%' -- Memblokir @, *, &, titik, spasi, dll.
);
GO

-- 4. Isi kembali dengan data awal yang bersih dan sah
INSERT INTO Master_PC (id_tier, nomor_pc, status) 
VALUES 
(1, 'PC-01', 'Tersedia'),
(1, 'PC-02', 'Tersedia'),
(2, 'VIP-01', 'Tersedia');
GO

-- 5. Perbarui juga tabel backup untuk tombol Reset aplikasi C#
IF OBJECT_ID('dbo.Master_PC_Backup') IS NOT NULL DROP TABLE dbo.Master_PC_Backup;
SELECT * INTO Master_PC_Backup FROM Master_PC;
GO

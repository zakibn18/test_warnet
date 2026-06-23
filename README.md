# 🛡️ Dokumentasi Simulasi SQL Injection - Sistem Warnet

Dokumentasi ini menjelaskan bagaimana aplikasi ini menangani celah keamanan SQL Injection (SQLi) serta strategi pertahanan yang diterapkan semoga mirip modul semungkin.

---

## 🚨 1. Celah Keamanan (The Vulnerability)
contoh pada fitur **Test Injection** untuk menunjukkan betapa bahayanya kode yang tidak aman.

* **Masalahnya:** Fitur ini menggunakan metode *String Concatenation* atau asal "tambah-tambahan" teks untuk merakit perintah database.
* **Kodenya:** `string query = "UPDATE Master_PC SET status='HACKED' WHERE nomor_pc='" + txtNoPC.Text + "'";`

### 💣 Skenario Serangan
Seorang penyerang bisa mengetikkan di kolom input:
`PC-01' OR '1'='1`

**Hasilnya?** Perintah yang dikirim ke database berubah menjadi perintah yang sangat kuat karena kondisi `'1'='1'` selalu dianggap benar. Akibatnya, status **seluruh PC** di database akan berubah menjadi 'HACKED' hanya dengan satu klik.

---

## 🛡️ 2. Strategi Pertahanan (Defense in Depth)
Untuk mencegah hal di atas terjadi pada fitur utama, kami menerapkan strategi pertahanan berlapis (Defense in Depth):

1.  **Lapis 1: Parameterized Query**
    Input dari user tidak langsung digabung ke teks query, melainkan dikirim melalui parameter khusus (`@nomor`). Ini memastikan input dianggap sebagai "teks biasa", bukan perintah yang bisa dijalankan.
2.  **Lapis 2: Stored Procedure**
    Logika query disembunyikan di dalam database. Aplikasi hanya tinggal memanggil namanya saja, sehingga struktur tabel asli tidak terekspos langsung.
3.  **Lapis 3: SQL VIEW**
    Untuk menampilkan data, aplikasi menggunakan VIEW (`vw_DataPC`) untuk membatasi akses. Ini memastikan user hanya bisa melihat apa yang memang diizinkan untuk dilihat.

---

## 🔄 3. Fitur Pemulihan (Reset Data)
Jika data rusak akibat simulasi serangan, kami menyediakan tombol **Reset** yang akan menghapus data yang tercemar dan mengembalikannya dari tabel cadangan secara instan.


---


1. Form koneksi 
<img width="292" height="200" alt="Screenshot 2026-04-14 230809" src="https://github.com/user-attachments/assets/bea8636f-4164-4269-95d5-67481dea7935" />


2. Form input data 
<img width="220" height="138" alt="Screenshot 2026-04-14 230959" src="https://github.com/user-attachments/assets/61797ce7-680d-49c4-8a22-49ee5c00f94e" />


3. Form tampilan data 
<img width="418" height="199" alt="Screenshot 2026-04-14 231101" src="https://github.com/user-attachments/assets/cad2bb4d-40d2-43c3-86a0-a266203054be" />


4. Bukti insert, update, delete, dan search
<img width="297" height="321" alt="Screenshot 2026-04-14 230851" src="https://github.com/user-attachments/assets/7a1a346a-c1cc-443d-8dd2-d192c7143f63" />
<img width="410" height="395" alt="Screenshot 2026-04-14 230919" src="https://github.com/user-attachments/assets/50b1483b-6d92-4c99-9c05-1d41eebfff4c" />
<img width="496" height="228" alt="Screenshot 2026-04-14 231040" src="https://github.com/user-attachments/assets/28f6f5e9-8879-4876-9d88-87775f15815e" />
<img width="418" height="199" alt="Screenshot 2026-04-14 231101" src="https://github.com/user-attachments/assets/69677587-8ecd-4f6a-9ed2-c27ebf390437" />
<img width="490" height="277" alt="Screenshot 2026-04-14 231051" src="https://github.com/user-attachments/assets/24fb42ea-80c1-4919-b4e4-d1d08b043c09" />
<img width="428" height="213" alt="Screenshot 2026-04-14 231114" src="https://github.com/user-attachments/assets/08967dad-d2a8-47a3-8b54-b24b579b39e3" />

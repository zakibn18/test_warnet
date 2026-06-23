using System;

namespace Sistem_Warnet
{
    // KUNCI: Wajib 'public' dan properti diletakkan langsung di dalam kelas ini
    public class DSWarnet
    {
        public DateTime tgl_transaksi { get; set; }
        public int durasi_jam { get; set; }
        public int total_bayar { get; set; }
        public string kode_voucher { get; set; }
        public string nomor_pc { get; set; }
        public string nama_tier { get; set; }
        public string nama_operator { get; set; }
    }
}
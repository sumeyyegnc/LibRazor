namespace LibRazor.Pages.Kitaplar
{
    public class Kitaplar
    {
        public int KitapId { get; set; }
        public string KitapAdi { get; set; }
        public decimal Fiyat { get; set; }   // 👈 BURASI ÖNEMLİ
        public int SayfaSayisi { get; set; }
        public string Yayinevi { get; set; }
    }
}

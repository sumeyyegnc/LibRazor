namespace LibRazor.Pages.Yazarlar
{
    public class Yazarlar
    {
        public int YazarId { get; set; }
        public string AdSoyad { get; set; }
        public int? DogumYili { get; set; }   // ✅ doğru
        public string Ulke { get; set; }
    }
}
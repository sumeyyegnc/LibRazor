namespace LibRazor.Pages.Auth
{
    public class Kullanici
    {
        public int Id { get; set; }
        public string AdSoyad { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Sifre { get; set; } = string.Empty;
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace LibRazor.Pages.Kitaplar
{
    public class CreateModel : PageModel
    {
        public Kitaplar kitapbilgi = new Kitaplar();

        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            // FORM VERÝLERÝ
            kitapbilgi.KitapAdi = Request.Form["KitapAdi"];
            kitapbilgi.Yayinevi = Request.Form["Yayinevi"];

            // INT / DECIMAL dönüþümleri
            int sayfa;
            decimal fiyat;

            if (!int.TryParse(Request.Form["SayfaSayisi"], out sayfa))
            {
                errorMessage = "Sayfa sayýsý geçersiz!";
                return;
            }

            if (!decimal.TryParse(Request.Form["Fiyat"], out fiyat))
            {
                errorMessage = "Fiyat geçersiz!";
                return;
            }

            kitapbilgi.SayfaSayisi = sayfa;
            kitapbilgi.Fiyat = fiyat;

            // VALIDATION
            if (string.IsNullOrWhiteSpace(kitapbilgi.KitapAdi) ||
                string.IsNullOrWhiteSpace(kitapbilgi.Yayinevi))
            {
                errorMessage = "Tüm alanlarý doldurunuz";
                return;
            }

            try
            {
                string connectionString =
                    "Server=(localdb)\\MSSQLLocalDB;Database=LibRazorDb;" +
                    "Integrated Security=true;TrustServerCertificate=true;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = @"
                        INSERT INTO Kitaplar (KitapAdi, Fiyat, SayfaSayisi, Yayinevi)
                        VALUES (@KitapAdi, @Fiyat, @SayfaSayisi, @Yayinevi)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@KitapAdi", kitapbilgi.KitapAdi);
                        command.Parameters.AddWithValue("@Fiyat", kitapbilgi.Fiyat);
                        command.Parameters.AddWithValue("@SayfaSayisi", kitapbilgi.SayfaSayisi);
                        command.Parameters.AddWithValue("@Yayinevi", kitapbilgi.Yayinevi);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            successMessage = "Kitap baþarýyla eklendi!";
            Response.Redirect("/Kitaplar/Index");
        }
    }
}
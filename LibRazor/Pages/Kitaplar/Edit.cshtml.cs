using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace LibRazor.Pages.Kitaplar
{
    public class EditModel : PageModel
    {
        public Kitaplar kitapbilgi = new Kitaplar();

        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            string connectionString =
                "Server=(localdb)\\MSSQLLocalDB;Database=LibRazorDb;" +
                "Integrated Security=true;TrustServerCertificate=true;";

            int id = 0;

            if (!int.TryParse(Request.Query["id"], out id))
            {
                Response.Redirect("/Kitaplar/Index");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM Kitaplar WHERE KitapId = @KitapId";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@KitapId", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                kitapbilgi.KitapId = reader.GetInt32(0);
                                kitapbilgi.KitapAdi = reader.IsDBNull(1) ? "" : reader.GetString(1);
                                kitapbilgi.Fiyat = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2);
                                kitapbilgi.SayfaSayisi = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
                                kitapbilgi.Yayinevi = reader.IsDBNull(4) ? "" : reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            kitapbilgi.KitapId = int.Parse(Request.Form["KitapId"]);
            kitapbilgi.KitapAdi = Request.Form["KitapAdi"];
            kitapbilgi.Yayinevi = Request.Form["Yayinevi"];

            if (!decimal.TryParse(Request.Form["Fiyat"], out decimal fiyat) ||
                !int.TryParse(Request.Form["SayfaSayisi"], out int sayfa))
            {
                errorMessage = "Fiyat veya Sayfa Sayýsý hatalý!";
                return;
            }

            kitapbilgi.Fiyat = fiyat;
            kitapbilgi.SayfaSayisi = sayfa;

            if (string.IsNullOrWhiteSpace(kitapbilgi.KitapAdi) ||
                string.IsNullOrWhiteSpace(kitapbilgi.Yayinevi))
            {
                errorMessage = "Tüm alanlar zorunludur";
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
                        UPDATE Kitaplar
                        SET KitapAdi = @KitapAdi,
                            Fiyat = @Fiyat,
                            SayfaSayisi = @SayfaSayisi,
                            Yayinevi = @Yayinevi
                        WHERE KitapId = @KitapId";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@KitapId", kitapbilgi.KitapId);
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

            successMessage = "Kitap baþarýyla güncellendi!";
            Response.Redirect("/Kitaplar/Index");
        }
    }
}
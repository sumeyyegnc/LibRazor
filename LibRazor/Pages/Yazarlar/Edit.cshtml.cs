using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace LibRazor.Pages.Yazarlar
{
    public class EditModel : PageModel
    {
        public Yazarlar yazarbilgi = new Yazarlar();

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
                Response.Redirect("/Yazarlar/Index");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM Yazarlar WHERE YazarId = @YazarId";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@YazarId", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                yazarbilgi.YazarId = reader.GetInt32(0);
                                yazarbilgi.AdSoyad = reader.IsDBNull(1) ? "" : reader.GetString(1);
                                yazarbilgi.DogumYili = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2);
                                yazarbilgi.Ulke = reader.IsDBNull(3) ? "" : reader.GetString(3);
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
            yazarbilgi.YazarId = int.Parse(Request.Form["YazarId"]);
            yazarbilgi.AdSoyad = Request.Form["AdSoyad"];
            yazarbilgi.Ulke = Request.Form["Ulke"];

            if (!int.TryParse(Request.Form["DogumYili"], out int dogumYili))
            {
                errorMessage = "Doðum yýlý hatalý!";
                return;
            }

            yazarbilgi.DogumYili = dogumYili;

            if (string.IsNullOrWhiteSpace(yazarbilgi.AdSoyad))
            {
                errorMessage = "Ad Soyad zorunludur";
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
                        UPDATE Yazarlar
                        SET AdSoyad = @AdSoyad,
                            DogumYili = @DogumYili,
                            Ulke = @Ulke
                        WHERE YazarId = @YazarId";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@YazarId", yazarbilgi.YazarId);
                        command.Parameters.AddWithValue("@AdSoyad", yazarbilgi.AdSoyad);
                        command.Parameters.AddWithValue("@DogumYili", yazarbilgi.DogumYili);
                        command.Parameters.AddWithValue("@Ulke", yazarbilgi.Ulke);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            successMessage = "Yazar baþarýyla güncellendi!";
            Response.Redirect("/Yazarlar/Index");
        }
    }
}
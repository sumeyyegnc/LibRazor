using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace LibRazor.Pages.Yazarlar
{
    public class CreateModel : PageModel
    {
        public Yazarlar yazarbilgi = new Yazarlar();

        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            // FORM VERÝLERÝ
            yazarbilgi.AdSoyad = Request.Form["AdSoyad"];
            yazarbilgi.Ulke = Request.Form["Ulke"];

            // INT dönüþüm
            int dogumYili;

            if (!int.TryParse(Request.Form["DogumYili"], out dogumYili))
            {
                errorMessage = "Doðum yýlý geçersiz!";
                return;
            }

            yazarbilgi.DogumYili = dogumYili;

            // VALIDATION
            if (string.IsNullOrWhiteSpace(yazarbilgi.AdSoyad))
            {
                errorMessage = "Ad Soyad zorunludur!";
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
                        INSERT INTO Yazarlar (AdSoyad, DogumYili, Ulke)
                        VALUES (@AdSoyad, @DogumYili, @Ulke)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@AdSoyad", yazarbilgi.AdSoyad);
                        command.Parameters.AddWithValue("@DogumYili", yazarbilgi.DogumYili);
                        command.Parameters.AddWithValue("@Ulke", yazarbilgi.Ulke ?? "");

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            successMessage = "Yazar baþarýyla eklendi!";
            Response.Redirect("/Yazarlar/Index");
        }
    }
}
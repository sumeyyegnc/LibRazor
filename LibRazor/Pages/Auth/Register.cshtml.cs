using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace LibRazor.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        public string ErrorMessage { get; set; } = string.Empty;
        public string SuccessMessage { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            string adsoyad = Request.Form["adsoyad"];
            string email = Request.Form["email"];
            string sifre = Request.Form["sifre"];

            if (string.IsNullOrEmpty(adsoyad) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(sifre))
            {
                ErrorMessage = "Tüm alanları doldurmanız gerekmektedir.";
                return Page();
            }

            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=LibRazorDb;Integrated Security=true;TrustServerCertificate=true;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Kullanicilar (AdSoyad, Email, Sifre) VALUES (@AdSoyad, @Email, @Sifre)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@AdSoyad", adsoyad);
                        command.Parameters.AddWithValue("@Email", email);
                        // In a real app, hash password
                        command.Parameters.AddWithValue("@Sifre", sifre);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Kayıt olurken bir hata oluştu: " + ex.Message;
                return Page();
            }

            SuccessMessage = "Başarıyla kayıt oldunuz. Giriş yapabilirsiniz.";
            return Page();
        }
    }
}

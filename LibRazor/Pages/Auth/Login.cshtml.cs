using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace LibRazor.Pages.Auth
{
    public class LoginModel : PageModel
    {
        public string ErrorMessage { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string email = Request.Form["email"];
            string sifre = Request.Form["sifre"];

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(sifre))
            {
                ErrorMessage = "Email ve Şifre boş bırakılamaz.";
                return Page();
            }

            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=LibRazorDb;Integrated Security=true;TrustServerCertificate=true;";
            bool isValid = false;
            string adSoyad = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT AdSoyad FROM Kullanicilar WHERE Email=@Email AND Sifre=@Sifre";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        // In a real application, you should hash the password!
                        command.Parameters.AddWithValue("@Sifre", sifre);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isValid = true;
                                adSoyad = reader.GetString(0);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Hata: " + ex.Message;
                return Page();
            }

            if (isValid)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, adSoyad),
                    new Claim(ClaimTypes.Email, email)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, 
                    new ClaimsPrincipal(claimsIdentity));

                return RedirectToPage("/Index");
            }
            else
            {
                ErrorMessage = "Geçersiz email veya şifre.";
                return Page();
            }
        }
    }
}

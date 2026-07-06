using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace LibRazor.Pages.Yazarlar
{
    public class IndexModel : PageModel
    {
        public List<Yazarlar> listele { get; set; } = new List<Yazarlar>();


        public void OnGet()
        {

            string connectionString =
                "Server=(localdb)\\MSSQLLocalDB;Database=LibRazorDb;" +
                "Integrated Security=true;TrustServerCertificate=true;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM Yazarlar";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Yazarlar yazar = new Yazarlar
                            {
                                YazarId = reader.GetInt32(0),
                                AdSoyad = reader.GetString(1),
                                DogumYili = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                                Ulke = reader.IsDBNull(3) ? "" : reader.GetString(3)
                            };

                            listele.Add(yazar);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace LibRazor.Pages.Kitaplar
{
    public class IndexModel : PageModel
    {
        public List<Kitaplar> listele { get; set; } = new List<Kitaplar>();
     

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

                    string sql = "SELECT * FROM Kitaplar";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Kitaplar kitap = new Kitaplar
                            {
                                KitapId = reader.GetInt32(0),
                                KitapAdi = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                Fiyat = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2),
                                SayfaSayisi = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                                Yayinevi = reader.IsDBNull(4) ? "" : reader.GetString(4)
                            };

                            listele.Add(kitap);
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
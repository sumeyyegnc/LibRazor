using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace LibRazor.Pages.Yazarlar
{
    public class DeleteModel : PageModel
    {
        public void OnGet()
        {
            string connectionString =
                "Server=(localdb)\\MSSQLLocalDB;Database=LibRazorDb;" +
                "Integrated Security=true;TrustServerCertificate=true;";

            // ID al
            int id = 0;

            if (!int.TryParse(Request.Query["id"], out id))
            {
                Response.Redirect("/Yazarlar/Index");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "DELETE FROM Yazarlar WHERE YazarId = @YazarId";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@YazarId", id);
                    command.ExecuteNonQuery();
                }
            }

            Response.Redirect("/Yazarlar/Index");
        }
    }
}
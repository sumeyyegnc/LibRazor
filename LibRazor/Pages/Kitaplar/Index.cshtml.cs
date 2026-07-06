using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using ClosedXML.Excel;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.IO;
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
        public IActionResult OnGetExportExcel()
        {
            OnGet();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Kitaplar");
                worksheet.Cell(1, 1).Value = "Kitap Id";
                worksheet.Cell(1, 2).Value = "Kitap Adı";
                worksheet.Cell(1, 3).Value = "Fiyat";
                worksheet.Cell(1, 4).Value = "Sayfa Sayısı";
                worksheet.Cell(1, 5).Value = "Yayınevi";

                int row = 2;
                foreach (var item in listele)
                {
                    worksheet.Cell(row, 1).Value = item.KitapId;
                    worksheet.Cell(row, 2).Value = item.KitapAdi;
                    worksheet.Cell(row, 3).Value = item.Fiyat;
                    worksheet.Cell(row, 4).Value = item.SayfaSayisi;
                    worksheet.Cell(row, 5).Value = item.Yayinevi;
                    row++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Kitaplar.xlsx");
                }
            }
        }

        public IActionResult OnGetExportPdf()
        {
            OnGet();
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header().Text("Kitaplar Listesi").SemiBold().FontSize(20).FontColor(Colors.Blue.Darken2);

                    page.Content().PaddingVertical(1, Unit.Centimetre).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(50);
                            columns.RelativeColumn();
                            columns.ConstantColumn(70);
                            columns.ConstantColumn(80);
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Id").SemiBold();
                            header.Cell().Text("Kitap Adı").SemiBold();
                            header.Cell().Text("Fiyat").SemiBold();
                            header.Cell().Text("Sayfa").SemiBold();
                            header.Cell().Text("Yayınevi").SemiBold();
                        });

                        foreach (var item in listele)
                        {
                            table.Cell().Text(item.KitapId.ToString());
                            table.Cell().Text(item.KitapAdi);
                            table.Cell().Text(item.Fiyat.ToString("C"));
                            table.Cell().Text(item.SayfaSayisi.ToString());
                            table.Cell().Text(item.Yayinevi);
                        }
                    });
                });
            });

            using (var stream = new MemoryStream())
            {
                document.GeneratePdf(stream);
                return File(stream.ToArray(), "application/pdf", "Kitaplar.pdf");
            }
        }
    }
}
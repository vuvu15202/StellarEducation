using System.Text;

namespace ASPNET_API.Services
{
    public interface IExportHTMLtoPDF
    {
        public string ToHtmlFile(List<string> data);
    }

    public class ExportHTMLtoPDF: IExportHTMLtoPDF 
    {
        public string ToHtmlFile(List<string> data)
        {
            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Services", "templateExport.html");
            string tempHtml = File.ReadAllText(templatePath);
            StringBuilder stringData = new StringBuilder(String.Empty);
            for (int i = 0; i < data.Count; i++)
            {
                stringData.Append($"<tr><td>{1}</td>");
                stringData.Append($"<td>{1}</td>");
                stringData.Append($"<td>{1}</td>");
                stringData.Append($"<td>{1}</td>");
                stringData.Append($"<td>{DateTime.Now.ToString("dd/MM/yyyy")}</td></tr>");
            }
            return tempHtml.Replace("{data}", stringData.ToString());
        }
    }
}

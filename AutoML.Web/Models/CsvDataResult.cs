namespace AutoML.Web.Models
{
    public class CsvDataResult
    {
        public required string FileName { get; set; }
        public required List<string> Columns { get; set; }
        public required List<Dictionary<string, string>> Rows { get; set; }
    }
}

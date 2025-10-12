namespace AutoML.Api.Models.Datasets
{
    public class ColumnDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Nullable { get; set; }
        public bool IsTarget { get; set; } = false; 
    }
}

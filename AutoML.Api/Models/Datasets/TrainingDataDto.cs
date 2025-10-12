namespace AutoML.Api.Models.Datasets
{
    public class TrainingDataDto
    {
        public Guid TenantId { get; set; }
        public Guid DatasetId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public List<ColumnDto> Columns { get; set; } = new();
        public List<Dictionary<string, object>> Preview { get; set; } = new();
    }
}

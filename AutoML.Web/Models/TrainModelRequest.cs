namespace AutoML.Web.Models
{
    public class TrainModelRequest
    {
        public string FileName { get; set; } = string.Empty;
        public double TestSplitRatio { get; set; } = 0.2f;
        public string? SpecificModelType { get; set; } = null;
        public string TargetColumn { get; set; } = string.Empty;
    }
}

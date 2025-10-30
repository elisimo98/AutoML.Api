namespace AutoML.Api.Models
{
    public class CreateModelConfigRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public double TestSize { get; set; } = 0.2f;
        public int RandomState { get; set; } = 42;
        public int Epochs { get; set; } = 50;
        public string ModelType { get; set; } = string.Empty;
        public string TargetColumn { get; set; } = string.Empty;
    }
}

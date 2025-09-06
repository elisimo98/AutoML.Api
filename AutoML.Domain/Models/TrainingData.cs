namespace AutoML.Domain.Models
{
    public class TrainingData
    {
        public List<List<float>> Features { get; set; } = new();
        public List<float> Labels { get; set; } = new();
    }
}

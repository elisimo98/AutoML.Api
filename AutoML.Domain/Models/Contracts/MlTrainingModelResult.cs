using System.Text.Json.Serialization;

namespace AutoML.Domain.Models.Contracts
{
    public class MlTrainingModelResult
    {
        [JsonPropertyName("model_type")]
        public string? ModelType { get; set; }

        [JsonPropertyName("accuracy")]
        public double Accuracy { get; set; }

        [JsonPropertyName("precision")]
        public double Precision { get; set; }

        [JsonPropertyName("recall")]
        public double Recall { get; set; }

        [JsonPropertyName("f1_score")]
        public double F1Score { get; set; }
    }
}

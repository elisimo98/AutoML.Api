namespace AutoML.Domain.Models.Contracts
{
    public class MlTrainingResponse
    {
        public List<MlTrainingModelResult> Results { get; set; } = new();
    }
}

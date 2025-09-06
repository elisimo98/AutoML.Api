namespace AutoML.Domain.Models.Contracts
{
    public class MLTrainingRequest
    {
        public string FileName { get; set; } = string.Empty;
        public double TestSplitRatio { get; set; } = 0.2f;
        public List<List<float>> Features { get; set; } = new();
        public List<float> Labels { get; set; } = new();

/*        public MLTrainingRequest(TrainModelRequest trainingRequest, TrainingData trainingData) 
        {
            if (trainingRequest == null) throw new ArgumentNullException(nameof(trainingRequest));

            FileName = trainingRequest.FileName;
            TestSplitRatio = trainingRequest.TestSplitRatio;
            Features = trainingData.Features;
            Labels = trainingData.Labels;
        }*/
    }
}

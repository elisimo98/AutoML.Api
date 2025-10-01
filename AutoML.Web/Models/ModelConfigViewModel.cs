namespace AutoML.Web.Models
{
    public class ModelConfigViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxTrainingEpochs { get; set; } = 10;
        public string TargetColumn { get; set; }
        public double TrainingSplit { get; set; } = 20.0;
        public string Framework { get; set; }
        public string ModelType { get; set; }
        public string Version { get; set; }
        public string Endpoint { get; set; }
        public string Environment { get; set; }
    }
}

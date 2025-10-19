using AutoML.Domain.Models.Contracts;

namespace AutoML.Domain.Interfaces
{
    public interface ITrainingClient
    {
        Task<TrainingResponse> TrainModelAsync(string fileName, double trainingRatio, string targetColumn, string modelType);
    }
}

using AutoML.Domain.Models.Contracts;

namespace AutoML.Web.Interfaces
{
    public interface IModelClientService
    {
        Task<TrainingResponse?> TrainModelAsync(string fileName, double trainingRatio, string targetColumn, string modelType);
    }
}

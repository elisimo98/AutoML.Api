using AutoML.Web.Enums;
using AutoML.Web.Models;

namespace AutoML.Web.Interfaces
{
    public interface IModelClientService
    {
        Task<TrainModelResponse?> TrainModelAsync(string fileName, double trainingRatio, string targetColumn, ModelType modelType);
    }
}

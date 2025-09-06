using AutoML.Domain.Models;

namespace AutoML.Web.Interfaces
{
    public interface ITrainingDataService
    {
        TrainingData ProcessCsv(Stream csvStream, string targetColumn);
    }
}

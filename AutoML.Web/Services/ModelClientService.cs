using AutoML.Domain.Models.Contracts;
using AutoML.Web.Interfaces;

namespace AutoML.Web.Services
{
    public class ModelClientService : IModelClientService
    {
        private readonly HttpClient client;

        public ModelClientService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<TrainingResponse?> TrainModelAsync(string fileName, double trainingRatio, string targetColumn, string modelType)
        {
            ArgumentException.ThrowIfNullOrEmpty(fileName);
            ArgumentException.ThrowIfNullOrEmpty(targetColumn);
            ArgumentException.ThrowIfNullOrEmpty(modelType);

            //var request = new TrainingRequest
            //{
            //    FileName = fileName,
            //    SpecificModelType = modelType,
            //    TestSplitRatio = trainingRatio,
            //};

            //var response = await client.PostAsJsonAsync("/ml/train", request);
            //response.EnsureSuccessStatusCode();
            //return await response.Content.ReadFromJsonAsync<TrainingResponse>();

            // todo: just returna dummy response for now
            return new TrainingResponse
            {
                Accuracy = 0.95,
                ModelType = modelType,
                Precision = 0.92,
                Recall = 0.93,
                F1Score = 0.94
            };
        }
    }
}

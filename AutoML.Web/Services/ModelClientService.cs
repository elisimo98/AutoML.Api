using AutoML.Web.Enums;
using AutoML.Web.Interfaces;
using AutoML.Web.Models;

namespace AutoML.Web.Services
{
    public class ModelClientService : IModelClientService
    {
        private readonly HttpClient client;

        public ModelClientService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<TrainModelResponse?> TrainModelAsync(string fileName, double trainingRatio, string targetColumn, ModelType modelType)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentException($"'{nameof(fileName)}' cannot be null or empty.", nameof(fileName));
            if (string.IsNullOrEmpty(targetColumn)) throw new ArgumentException($"'{nameof(targetColumn)}' cannot be null or empty.", nameof(targetColumn));

            var request = new TrainModelRequest
            {
                FileName = fileName,
                SpecificModelType = modelType != ModelType.All ? modelType.ToString() : null,
                TestSplitRatio = trainingRatio,
            };

            var response = await client.PostAsJsonAsync("/ml/train", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TrainModelResponse>();
        }
    }
}

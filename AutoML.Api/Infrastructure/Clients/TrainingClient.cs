using AutoML.Domain.Interfaces;
using AutoML.Domain.Models.Contracts;

namespace AutoML.Web.Services
{
    public class TrainingClient : ITrainingClient
    {
        private readonly HttpClient client;

        public TrainingClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<TrainingResponse?> TrainModelAsync(string fileName, double testSize, string targetColumn, string modelType)
        {
            ArgumentException.ThrowIfNullOrEmpty(fileName);
            ArgumentException.ThrowIfNullOrEmpty(targetColumn);
            ArgumentException.ThrowIfNullOrEmpty(modelType);

            var request = new TrainingRequest
            {
                FileName = fileName,
                TargetColumn = targetColumn,
                ModelType = modelType,
                TestSize = testSize,
                RandomState = 42
            };

            var response = await client.PostAsJsonAsync("/train", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TrainingResponse>();
        }
    }
}

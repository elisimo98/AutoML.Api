namespace AutoML.Api.Models
{
    public class ServiceResult<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public List<string> Errors { get; set; } = new();

        public static ServiceResult<T> Success(T data) =>
            new() { IsSuccess = true, Data = data };

        public static ServiceResult<T> Failure(params string[] errors) =>
            new() { IsSuccess = false, Errors = errors.ToList() };
    }
}

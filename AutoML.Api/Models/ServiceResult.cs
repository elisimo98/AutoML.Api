namespace AutoML.Api.Models
{
    public class ServiceResult
    {
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; } = [];

        public static ServiceResult Success() => new() { IsSuccess = true };
        public static ServiceResult Failure(params string[] errors) => new() { IsSuccess = false, Errors = errors.ToList() };
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; }

        public static ServiceResult<T> Success(T data) => new() { IsSuccess = true, Data = data };
        public new static ServiceResult<T> Failure(params string[] errors) => new() { IsSuccess = false, Errors = errors.ToList() };
    }

}

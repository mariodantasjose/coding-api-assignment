namespace QuestionnaireAPI.Application
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public bool NotFound { get; set; }
        public bool IsError { get; set; }
        public string? ErrorMessage { get; set; }

        public static ServiceResponse<T> SuccessResult(T data, string response)
        {
            return new ServiceResponse<T>
            {
                Success = true,
                Data = data
            };
        }

        public static ServiceResponse<T> NotFoundResult()
        {
            return new ServiceResponse<T>
            {
                NotFound = true
            };
        }

        public static ServiceResponse<T> ErrorResult(string errorMessage)
        {
            return new ServiceResponse<T>
            {
                ErrorMessage = errorMessage,
                IsError = true
            };
        }
    }
}
using FluentValidation.Results;

namespace ChatSystem_Application.Responses
{
    public class BaseCommandResponse
    {
        public object Data { get; set; } = null!;
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;

        public int StatusCode { get; set; }

        public List<string>? Errors { get; set; } = new List<string>();


        public void Success(object? data = null, string? message = null, List<string>? errors = null)
        {
            IsSuccess = true;
            Message = message ?? "Success...";
            Errors = errors;
            StatusCode = 200;
            Data = data;

        }

        public void Delete(object? data = null, string? message = null, List<string>? errors = null)
        {
            IsSuccess = true;
            Message = message ?? "Deleted...";
            Errors = errors;
            StatusCode = 204;
            Data = data;
        }

        public void Update(object? data = null, string? message = null, List<string>? errors = null)
        {
            IsSuccess = true;
            Message = message ?? "Updated...";
            Errors = errors;
            StatusCode = 204;
            Data = data;
        }

        public void ServerError(object? data = null, string? message = null, List<string>? errors = null)
        {
            IsSuccess = false;
            Message = message ?? "Error In Server...";
            Errors = errors;
            StatusCode = 500;
            Data = data;
        }
        public void Forbid(object? data = null, string? message = null, List<string>? errors = null)
        {
            IsSuccess = false;
            Message = message ?? "Forbid...";
            Errors = errors;
            StatusCode = 403;
            Data = data;
        }
        public void Failure(object? data = null, string? message = null, List<string>? errors = null)
        {
            IsSuccess = false;
            Message = message ?? "Failure...";
            Errors = errors;
            StatusCode = -1;
            Data = data;
        }

        public void NotFound(object? data = null, string? message = null, List<string>? errors = null)
        {
            IsSuccess = false;
            Message = message ?? "Not found";
            Errors = errors;
            StatusCode = 404;
            Data = data;
        }

        public void BadRequest(List<string>? errors = null)
        {
            IsSuccess = false;
            Message = "Bad Request";
            Errors = errors;
            StatusCode = 400;
            Data = null;
        }



        public List<string> ConvertValidationFailureToLisString
            (List<ValidationFailure> validationFailures)
        {
            List<string> result = new List<string>();
            foreach (var validationFailure in validationFailures)
            {
                var res = validationFailure.ToString();
                result.Add(res);
            }
            return result;
        }


    }
}

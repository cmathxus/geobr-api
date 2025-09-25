namespace GeoBR.Application.UseCases
{
  public class ServiceResult<T>
  {
    public bool Result { get; private set; }
    public string? Message { get; private set; } = string.Empty;
    public T? Data { get; private set; }

    public static ServiceResult<T> Success(T data) => new() { Result = true, Data = data };
    public static ServiceResult<T> Fail(string message) => new() { Result = false, Message = message };
  }
}

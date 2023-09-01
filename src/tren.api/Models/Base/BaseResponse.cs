namespace tren.api.Models.Base;

public class BaseResponse<T> where T : new()
{
    public BaseResponse()
    {
        Data = new T();
    }

    public bool HasError => Errors.Any();

    public List<string> Errors { get; set; } = new();

    public T Data { get; set; }

    public long Total { get; set; }

    public BaseResponse<T> SetData(T data)
    {
        Data = data;
        return this;
    }

    public BaseResponse<T> SetError(string errorMessage)
    {
        Errors.Add(errorMessage);
        return this;
    }
}
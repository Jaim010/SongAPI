namespace Song.API.Models
{
  public class ServiceResponse<T>
  {
    public T? Data { get; set; }
    public Result Result { get; set; }
  }

  public class ServiceResponse
  {
    public Result Result { get; set; }
  }
}
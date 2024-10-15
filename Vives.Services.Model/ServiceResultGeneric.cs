namespace Vives.Services.Model
{
    public class ServiceResult<T>: ServiceResult
    {

        public ServiceResult(T? data = default)
        {
            Data = data;
        }
        public T? Data { get; set; }
    }
}

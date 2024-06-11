namespace Services.B.Core.Dtos
{
    public class ServiceResponse<T> : ServiceResponseBase
    {
        public T Data { get; set; }
    }
}

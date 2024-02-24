namespace MyEcomWebApplication.Application.Interface
{
    public interface IDeliveryService
    {
        Task<object> GetRecentOrderAsync(string user, string customerId);
    }
}

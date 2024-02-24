// DeliveryService.cs
using MyEcomWebApplication.Application.Interface;

public class DeliveryService : IDeliveryService
{
    private readonly IDeliveryRepository _repository;

    public DeliveryService(IDeliveryRepository repository)
    {
        _repository = repository;
    }

    public async Task<object> GetRecentOrderAsync(string user, string customerId)
    {
        var order = await _repository.GetMostRecentOrderAsync(customerId);
        if (order == null)
        {
            return new { customer = new { firstName = "", lastName = "" }, order = new { } };
        }
        else
        {
            return order;
        }
    }
}

using MyEcomWebApplication.Application.Common.Models;
using MyEcomWebApplication.Application.DataAccess;
using MyEcomWebApplication.Application.Interface;


public class DeliveryRepository : IDeliveryRepository
{
    public IDeliveryRepository _deliveryRepository;
    public DataAccess _dataAccess;

    public DeliveryRepository(string connectionString, IDeliveryRepository deliveryRepository, DataAccess dataAccess)
    {
        _deliveryRepository = deliveryRepository;
        _dataAccess = dataAccess;

    }

    public async Task<OrderResponseModel> GetMostRecentOrderAsync(string customerId)
    {
        var result = _dataAccess.GetMostRecentOrderAsync(customerId);
        return await result;
    }
}

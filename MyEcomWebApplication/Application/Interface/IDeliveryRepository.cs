using MyEcomWebApplication.Application.Common.Models;

namespace MyEcomWebApplication.Application.Interface
{
    public interface IDeliveryRepository
    {
        public Task<OrderResponseModel> GetMostRecentOrderAsync(string customerId);
    }
}

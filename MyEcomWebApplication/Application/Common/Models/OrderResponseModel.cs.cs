namespace MyEcomWebApplication.Application.Common.Models
{
    public class OrderResponseModel
    {
        public Customer? customer { get; set; }
        public Order? order { get; set; }
    }

    public class ItemDetails
    {
        public string? Product { get; set; }
        public int? Quantity { get; set; }
        public int? PriceEach { get; set; }

    }

    public class Customer
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

    }

    public class Order
    {
        public int?  OrderNumber { get; set; }
        public string? OrderDate { get; set; }
        public string? DeliveryAddress { get; set; }

        public List <ItemDetails>? OrderItems { get; set; }

       public string? DeliveryExpected { get; set; }
        public bool? ContainsGift { get; set; }

    }

}

using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using MyEcomWebApplication.Application.Common.Models;
using MyEcomWebApplication.Application.Interface;

namespace MyEcomWebApplication.Application.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;
        private readonly IValidator<CustomerOrderRequest> _validator;


        public DeliveryController(IDeliveryService deliveryService, IValidator<CustomerOrderRequest> validator)
        {
            _deliveryService = deliveryService;
            _validator = validator;

        }

        [HttpPost]
        public async Task<IActionResult> GetRecentOrder([FromBody] CustomerOrderRequest request)
        {
            // Validate the request
            ValidationResult validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await _deliveryService.GetRecentOrderAsync(request.User, request.CustomerId);
            if (result == null)
            {
                throw new Exception("No orders found for the customer.");
            }
            else
            {
                return Ok(result);
            }
            
        }
    }
}


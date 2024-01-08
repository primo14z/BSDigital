using BSDigital.Models;
using BSDigital.Service;
using Microsoft.AspNetCore.Mvc;

namespace BSDigital;

[ApiController]
[Route("api/[controller]")]
public class ApiController : ControllerBase
{
    private IOrderService _orderService;

    public ApiController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public IActionResult PlaceOrder(OrderInput orderInput)
    {
        var result = _orderService.GenerateOrder(orderInput);

        return Ok(result);
    }
}
using AutoMapper;
using HungryHUB.DTO;
using HungryHUB.Entity;
using HungryHUB.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using log4net;
using System;
using System.Collections.Generic;

namespace HungryHUB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;

        public OrderController(IOrderService orderService, IMapper mapper, ILog logger = null)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult CreateOrder([FromBody] OrderDTO orderDTO)
        {
            try
            {
                var order = _mapper.Map<Order>(orderDTO);
                _orderService.CreateOrder(order);
                _logger?.Info($"Order created successfully. Order ID: {order.OrderId}");
                return StatusCode(200, order.OrderId);
            }
            catch (Exception ex)
            {
                _logger?.Error($"Error in CreateOrder: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{orderId}")]
        [Authorize(Roles = "User, Restaurant, Delivery")]
        public IActionResult GetOrderById(int orderId)
        {
            try
            {
                var order = _orderService.GetOrderById(orderId);

                if (order == null)
                {
                    _logger?.Warn($"Order with ID {orderId} not found.");
                    return NotFound();
                }

                var orderDTO = _mapper.Map<OrderDTO>(order);
                _logger?.Info($"Retrieved order by ID successfully. Order ID: {orderId}");
                return StatusCode(200, orderDTO);
            }
            catch (Exception ex)
            {
                _logger?.Error($"Error in GetOrderById: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "User, Restaurant, Delivery")]
        public IActionResult GetAllOrders()
        {
            try
            {
                var orders = _orderService.GetAllOrders();
                var orderDTOs = _mapper.Map<List<OrderDTO>>(orders);
                _logger?.Info("Retrieved all orders successfully.");
                return StatusCode(200, orderDTOs);
            }
            catch (Exception ex)
            {
                _logger?.Error($"Error in GetAllOrders: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{orderId}")]
        [Authorize(Roles = "User, Restaurant")]
        public IActionResult UpdateOrder(int orderId, [FromBody] OrderDTO updatedOrderDTO)
        {
            try
            {
                var updatedOrder = _mapper.Map<Order>(updatedOrderDTO);
                _orderService.UpdateOrder(orderId, updatedOrder);
                _logger?.Info($"Order updated successfully. Order ID: {orderId}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger?.Error($"Error in UpdateOrder: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{orderId}")]
        [Authorize(Roles = "User, Restaurant")]
        public IActionResult DeleteOrder(int orderId)
        {
            _orderService.DeleteOrder(orderId);
            _logger?.Info($"Order deleted successfully. Order ID: {orderId}");
            return NoContent();
        }
    }
}

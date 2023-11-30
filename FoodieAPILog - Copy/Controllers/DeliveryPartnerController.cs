using AutoMapper;
using HungryHUB.DTO;
using HungryHUB.Entity;
using HungryHUB.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using log4net;


[Route("api/[controller]")]
[ApiController]
public class DeliveryPartnerController : ControllerBase
{
    private readonly IDeliveryPartnerService _deliveryPartnerService;
    private readonly IMapper _mapper;
    private readonly ILog _logger;

    public DeliveryPartnerController(IDeliveryPartnerService deliveryPartnerService, IMapper mapper, ILog logger = null)
    {
        _deliveryPartnerService = deliveryPartnerService ?? throw new ArgumentNullException(nameof(deliveryPartnerService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger;
    }

    [HttpGet, Route("GetAllDeliveryPartners")]
    [Authorize(Roles = "Admin, Delivery")]
    public IActionResult GetAllDeliveryPartners()
    {
        try
        {
            List<DeliveryPartner> deliveryPartners = _deliveryPartnerService.GetAllDeliveryPartners();
            List<DeliveryPartnerDTO> deliveryPartnersDto = _mapper.Map<List<DeliveryPartnerDTO>>(deliveryPartners);
            _logger?.Info("Retrieved all delivery partners successfully.");
            return Ok(deliveryPartnersDto);
        }
        catch (Exception ex)
        {
            _logger?.Error($"Error in GetAllDeliveryPartners: {ex.Message}");
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost, Route("CreateDeliveryPartner")]
    [Authorize(Roles = "Delivery")]
    public IActionResult CreateDeliveryPartner(DeliveryPartnerDTO deliveryPartnerDto)
    {
        try
        {
            DeliveryPartner deliveryPartner = _mapper.Map<DeliveryPartner>(deliveryPartnerDto);
            _deliveryPartnerService.CreateDeliveryPartner(deliveryPartner);
            _logger?.Info("Delivery partner created successfully.");
            return StatusCode(201, deliveryPartner);
        }
        catch (Exception ex)
        {
            _logger?.Error($"Error in CreateDeliveryPartner: {ex.Message}");
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut, Route("UpdateDeliveryPartner/{deliveryPartnerId}")]
    [Authorize(Roles = "Admin, Delivery")]
    public IActionResult UpdateDeliveryPartner(int deliveryPartnerId, DeliveryPartnerDTO deliveryPartnerDto)
    {
        try
        {
            var existingDeliveryPartner = _deliveryPartnerService.GetDeliveryPartnerById(deliveryPartnerId);

            if (existingDeliveryPartner == null)
            {
                _logger?.Warn($"Delivery partner with ID {deliveryPartnerId} not found.");
                return NotFound();
            }

            var updatedDeliveryPartner = _mapper.Map<DeliveryPartner>(deliveryPartnerDto);
            _deliveryPartnerService.UpdateDeliveryPartner(deliveryPartnerId, updatedDeliveryPartner);

            _logger?.Info($"Delivery partner updated successfully. Delivery Partner ID: {deliveryPartnerId}");
            return Ok(updatedDeliveryPartner);
        }
        catch (Exception ex)
        {
            _logger?.Error($"Error in UpdateDeliveryPartner: {ex.Message}");
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete, Route("DeleteDeliveryPartner/{deliveryPartnerId}")]
    [Authorize(Roles = "Admin, Delivery")]
    public IActionResult DeleteDeliveryPartner(int deliveryPartnerId)
    {
        var existingDeliveryPartner = _deliveryPartnerService.GetDeliveryPartnerById(deliveryPartnerId);

        if (existingDeliveryPartner == null)
        {
            _logger?.Warn($"Delivery partner with ID {deliveryPartnerId} not found.");
            return NotFound();
        }

        _deliveryPartnerService.DeleteDeliveryPartner(deliveryPartnerId);

        _logger?.Info($"Delivery partner deleted successfully. Delivery Partner ID: {deliveryPartnerId}");
        return NoContent();
    }
}

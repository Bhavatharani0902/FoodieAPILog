using AutoMapper;
using HungryHUB.DTO;
using HungryHUB.Entity;
using HungryHUB.Service;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HungryHUB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _couponService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;

        public CouponController(ICouponService couponService, IMapper mapper, ILog logger = null)
        {
            _couponService = couponService ?? throw new ArgumentNullException(nameof(couponService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger;
        }

        [HttpGet, Route("GetAllCoupons")]
        [Authorize(Roles = "Admin, User")]
        public IActionResult GetAllCoupons()
        {
            try
            {
                List<Coupon> coupons = _couponService.GetAllCoupons();
                List<CouponDTO> couponDTOs = _mapper.Map<List<CouponDTO>>(coupons);
                _logger?.Info("Retrieved all coupons successfully.");
                return StatusCode(200, couponDTOs);
            }
            catch (Exception ex)
            {
                _logger?.Error($"Error in GetAllCoupons: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet, Route("GetCouponById/{couponId}")]
        [Authorize(Roles = "Admin, User")]
        public IActionResult GetCouponById(int couponId)
        {
            try
            {
                var coupon = _couponService.GetCouponById(couponId);

                if (coupon == null)
                {
                    _logger?.Warn($"Coupon with ID {couponId} not found.");
                    return StatusCode(404, "Coupon not found");
                }

                var couponDTO = _mapper.Map<CouponDTO>(coupon);
                _logger?.Info($"Retrieved coupon by ID successfully. Coupon ID: {couponId}");
                return StatusCode(200, couponDTO);
            }
            catch (Exception ex)
            {
                _logger?.Error($"Error in GetCouponById: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost, Route("CreateCoupon")]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateCoupon([FromBody] CouponDTO couponDTO)
        {
            try
            {
                var coupon = _mapper.Map<Coupon>(couponDTO);
                _couponService.CreateCoupon(coupon);
                _logger?.Info("Coupon created successfully.");
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                _logger?.Error($"Error in CreateCoupon: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete, Route("DeleteCoupon/{couponId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCoupon(int couponId)
        {
            try
            {
                var existingCoupon = _couponService.GetCouponById(couponId);

                if (existingCoupon == null)
                {
                    _logger?.Warn($"Coupon with ID {couponId} not found.");
                    return StatusCode(404, "Coupon not found");
                }

                _couponService.DeleteCoupon(couponId);
                _logger?.Info($"Coupon deleted successfully. Coupon ID: {couponId}");
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                _logger?.Error($"Error in DeleteCoupon: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}

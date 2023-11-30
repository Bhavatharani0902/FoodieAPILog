using AutoMapper;
using HungryHUB.DTO;
using HungryHUB.Entity;
using HungryHUB.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using log4net;
using Microsoft.AspNetCore.Authorization;

namespace HungryHUB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;

        public WalletController(IWalletService walletService, IMapper mapper, ILog logger = null)
        {
            _walletService = walletService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult CreateWallet([FromBody] WalletDTO walletDTO)
        {
            try
            {
                var wallet = _mapper.Map<Wallet>(walletDTO);
                _walletService.CreateWallet(wallet);
                _logger?.Info($"Wallet created successfully. Wallet ID: {wallet.WalletId}");
                return StatusCode(200, wallet.WalletId);
            }
            catch (Exception ex)
            {
                _logger?.Error($"Error creating wallet: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{walletId}")]
        [Authorize(Roles = "Admin, User")]
        public IActionResult GetWalletById(int walletId)
        {
            try
            {
                var wallet = _walletService.GetWalletById(walletId);

                if (wallet == null)
                {
                    _logger?.Warn($"Wallet with ID {walletId} not found.");
                    return NotFound();
                }

                var walletDTO = _mapper.Map<WalletDTO>(wallet);
                _logger?.Info($"Retrieved wallet by ID successfully. Wallet ID: {walletId}");
                return StatusCode(200, walletDTO);
            }
            catch (Exception ex)
            {
                _logger?.Error($"Error getting wallet by ID: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllWallets()
        {
            try
            {
                var wallets = _walletService.GetAllWallets();
                var walletDTOs = _mapper.Map<List<WalletDTO>>(wallets);
                _logger?.Info("Retrieved all wallets successfully.");
                return StatusCode(200, walletDTOs);
            }
            catch (Exception ex)
            {
                _logger?.Error($"Error getting all wallets: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{walletId}")]
        [Authorize(Roles = "Admin, User")]
        public IActionResult UpdateWallet(int walletId, [FromBody] WalletDTO updatedWalletDTO)
        {
            try
            {
                var updatedWallet = _mapper.Map<Wallet>(updatedWalletDTO);
                _walletService.UpdateWallet(walletId, updatedWallet);
                _logger?.Info($"Wallet updated successfully. Wallet ID: {walletId}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger?.Error($"Error updating wallet: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{walletId}")]
        [Authorize(Roles = "Admin, User")]
        public IActionResult DeleteWallet(int walletId)
        {
            try
            {
                _walletService.DeleteWallet(walletId);
                _logger?.Info($"Wallet deleted successfully. Wallet ID: {walletId}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger?.Error($"Error deleting wallet: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}

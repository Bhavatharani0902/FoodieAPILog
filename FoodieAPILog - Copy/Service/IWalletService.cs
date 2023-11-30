
using HungryHUB.Entity;

namespace HungryHUB.Service
{
    public interface IWalletService
    {
        void CreateWallet(Wallet wallet);
        void UpdateWallet(int walletId, Wallet updatedWallet);
        void DeleteWallet(int walletId);
        Wallet GetWalletById(int walletId);
        List<Wallet> GetAllWallets();
        List<Wallet> GetWalletsByUser(int userID);
    }
}
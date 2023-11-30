using HungryHUB.Database;
using HungryHUB.Entity;

namespace HungryHUB.Service
{
    public class DeliveryPartnerService : IDeliveryPartnerService
    {
        private readonly MyContext _context;

        public DeliveryPartnerService(MyContext context)
        {
            _context = context;
        }

        public List<DeliveryPartner> GetAllDeliveryPartners()
        {
            return _context.DeliveryPartners.ToList();
        }

        public DeliveryPartner GetDeliveryPartnerById(int deliveryPartnerId)
        {
            return _context.DeliveryPartners.Find(deliveryPartnerId);
        }

        public void CreateDeliveryPartner(DeliveryPartner deliveryPartner)
        {
            try
            {
                _context.DeliveryPartners.Add(deliveryPartner);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateDeliveryPartner(int deliveryPartnerId, DeliveryPartner updatedDeliveryPartner)
        {
            var existingDeliveryPartner = _context.DeliveryPartners.Find(deliveryPartnerId);

            if (existingDeliveryPartner != null)
            {
                existingDeliveryPartner.Name = updatedDeliveryPartner.Name;
                existingDeliveryPartner.PhoneNumber = updatedDeliveryPartner.PhoneNumber;
                existingDeliveryPartner.IsAvailable = updatedDeliveryPartner.IsAvailable;

                _context.DeliveryPartners.Update(existingDeliveryPartner);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException($"DeliveryPartner with ID {deliveryPartnerId} not found.");
            }
        }

        public void DeleteDeliveryPartner(int deliveryPartnerId)
        {
            var deliveryPartner = _context.DeliveryPartners.Find(deliveryPartnerId);

            if (deliveryPartner != null)
            {
                _context.DeliveryPartners.Remove(deliveryPartner);
                _context.SaveChanges();
            }
        }
    }
}

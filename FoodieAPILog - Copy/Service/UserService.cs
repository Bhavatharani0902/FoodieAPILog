using HungryHUB.Database;
using HungryHUB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HungryHUB.Service
{
    public class UserService : IUserService
    {
        private readonly MyContext _context;

        public UserService(MyContext context)
        {
            _context = context;
        }

        public void CreateUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteUser(int userID)
        {
            User user = _context.Users.Find(userID);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public void EditUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUser(int userID)
        {
            return _context.Users.Find(userID);
        }

        public User ValidateUser(string email, string password)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email && u.Password == password);
        }
    }
}

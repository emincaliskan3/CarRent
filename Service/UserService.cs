using Data;
using Entities;
using System.Linq;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly DatabaseContext _context;

        public UserService(DatabaseContext context)
        {
            _context = context;
        }

        public User Authenticate(string email, string password)
        {
           
            var user = _context.Users.SingleOrDefault(u => u.Email == email);

           
            if (user == null || user.Password != password)
                return null;

           
            return user;
        }

       

        public void ForgotPassword(string email)
        {
            
            throw new NotImplementedException();
        }

        public bool ResetPassword(string email, string token, string newPassword)
        {
            
            throw new NotImplementedException();
        }

        public string GetCartContent(int userId)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserCart(int userId, string cartContent)
        {
            throw new NotImplementedException();
        }

        public void ClearUserCart(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PactServer
{
    public class UserService
    {
        private readonly UserContext _userContext;

        public UserService()
        {
            var optionsBuilder = new DbContextOptionsBuilder<UserContext>();
            var connection = @"Server=DESKTOP-LPO9T14\SQLEXPRESS;Database=Pact;Trusted_Connection=True;ConnectRetryCount=0";
            optionsBuilder.UseSqlServer(connection);
            _userContext = new UserContext(optionsBuilder.Options);
            
        }

        public List<UserModel> GetUsers()
        {
            return _userContext.Users.ToList();
        }

        public UserModel GetUser(string id)
        {
            return _userContext.Users.SingleOrDefault(user => user.Id == id);
        }

        public void AddUser(UserModel value)
        {
            _userContext.Users.Add(value);
            _userContext.SaveChanges();
        }

        public void DeleteUser(string id)
        {
            var user = GetUser(id);
            _userContext.Users.Remove(user);
            _userContext.SaveChanges();
        }
    }
}

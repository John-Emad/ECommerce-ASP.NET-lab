using Market.Data;
using Market.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Repository
{
    public class UserRepository : IUserRepository
    {
        MarketDbContext db;

        public UserRepository(MarketDbContext _db)
        {
            db = _db;
        }

        public void Add(User user)
        {
            db.Add(user);
        }

        public void Delete(User user)
        {
           db.Remove(user);
        }

        public List<User> GetAll()
        {
            return db.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .ToList();
        }

        public User GetByEmailAndPassword(string email, string password)
        {
            return db.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefault(u => u.Email == email && u.Password == password);
        }


        public User GetById(int id)
        {
            return db.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefault(u => u.Id == id);
        }

        public int Save()
        {
            return db.SaveChanges();
        }

        public Role GetUserRoleById(int roleId)
        {
            return db.Roles.FirstOrDefault(r => r.Id == roleId);
        }

        public void Update(User user)
        {
            db.Users.Update(user);
        }
    }
}

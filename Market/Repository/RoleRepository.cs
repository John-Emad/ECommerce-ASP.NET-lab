using Market.Data;
using Market.Models;

namespace Market.Repository
{
    public class RoleRepository : IRoleRepository
    {
        MarketDbContext db;

        public RoleRepository(MarketDbContext _db)
        {
            db = _db;
        }

        public void Add(Role role)
        {
            db.Roles.Add(role);
        }

        public List<Role> GetAll()
        {
            return db.Roles.ToList();
        }

        public Role GetById(int id)
        {
            return db.Roles.FirstOrDefault(r => r.Id == id);
        }

        public void Remove(Role role)
        {
            db.Roles.Remove(role);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}

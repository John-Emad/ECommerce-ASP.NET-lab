using Market.Data;
using Market.Models;

namespace Market.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        MarketDbContext db;

        public CustomerRepository(MarketDbContext _db)
        {
            db = _db;
        }

        public void Add(Customer customer)
        {
            db.Add(customer);
        }

        public void Delete(Customer customer)
        {
            db.Remove(customer);
        }

        public List<Customer> GetAll()
        {
            return db.Customers.ToList();
        }

        public Customer GetById(int id)
        {
            return db.Customers.FirstOrDefault(c => c.Id == id);
        }

        public int Save()
        {
            return db.SaveChanges();
        }

        public void Update(Customer customer)
        {
            db.Update(customer);
        }
    }
}

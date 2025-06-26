using Market.Data;
using Market.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Repository
{
    public class ProductRepository : IProductRepository
    {
        MarketDbContext db;

        public ProductRepository(MarketDbContext _db)
        {
            db = _db;
        }

        public void Add(Product product)
        {
            db.Add(product);
        }

        public void Delete(Product product)
        {
            db.Remove(product);
        }

        public List<Product> GetAll()
        {
            return db.Products.Include(p => p.Category).ToList();
        }

        public Product GetById(int id)
        {
            return db.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
        }

        public int Save()
        {
            return db.SaveChanges();
        }

        public void Update(Product product)
        {
            db.Update(product);
        }
    }
}

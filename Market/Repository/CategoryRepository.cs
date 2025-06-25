using Market.Data;
using Market.Models;

namespace Market.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        MarketDbContext db = new MarketDbContext();

        public void Add(Category category)
        {
            db.Add(category);
        }

        public void Delete(Category category)
        {
            db.Remove(category);
        }

        public List<Category> GetAll()
        {
            return db.Categories.ToList();
        }

        public Category GetById(int id)
        {
            return db.Categories.FirstOrDefault(c => c.Id == id);
        }

        public int Save()
        {
            return db.SaveChanges();
        }

        public void Update(Category category)
        {
            db.Update(category);
        }
    }
}

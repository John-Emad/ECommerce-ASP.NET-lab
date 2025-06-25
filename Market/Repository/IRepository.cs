using Market.Models;

namespace Market.Repository
{
    public interface ICustomerRepository
    {
        public void Add(Customer customer);

        public Customer GetById(int id);

        public List<Customer> GetAll();

        public void Update(Customer customer);

        public void Delete(Customer customer);

        public int Save();


    }

    public interface IProductRepository
    {
        public void Add(Product product);

        public Product GetById(int id);

        public List<Product> GetAll();

        public void Update(Product product);

        public void Delete(Product product);

        public int Save();

    }

    public interface ICategoryRepository
    {
        public void Add(Category category);

        public Category GetById(int id);

        public List<Category> GetAll();

        public void Update(Category category);

        public void Delete(Category category);

        public int Save();

    }
}

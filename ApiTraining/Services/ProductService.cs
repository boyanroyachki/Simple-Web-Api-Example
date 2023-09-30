using ApiTraining.Data;

namespace ApiTraining.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductDbContext data;
        public ProductService(ProductDbContext data) => this.data = data;

        public Product CreateProduct(string name, string description)
        {
            var newProduct = new Product() { Name = name, Description = description };
            this.data.Products.Add(newProduct);
            this.data.SaveChanges();
            return newProduct;
        }

        public Product DeleteProduct(int id)
        {
            var dbProduct = GetById(id);
            this.data.Products.Remove(dbProduct);
            this.data.SaveChanges(); 

            return dbProduct;
        }

        public void EditProduct(int id, Product product)
        {
            var dbProduct = GetById(id);
            dbProduct.Name = product.Name;
            dbProduct.Description = product.Description;

            this.data.SaveChanges();
        }

        public void EditProductPartially(int id, Product product)
        {
            var dbProduct = GetById(id);
           
            dbProduct.Name = String.IsNullOrEmpty(product.Name) 
                ? dbProduct.Name : product.Name;

            dbProduct.Description = String.IsNullOrEmpty(product.Description) 
                ? dbProduct.Description : product.Description;

            this.data.SaveChanges();
        }

        public List<Product> GetAllProducts()
        {
            return this.data.Products.ToList();
        }

        public Product GetById(int id) =>this.data.Products.Find(id);
        
    }
}

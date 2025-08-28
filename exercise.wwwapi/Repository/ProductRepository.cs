public class ProductRepository
{
    private readonly List<Product> _products = new();
    private int _nextId = 1;

    public ProductRepository()
    {
        _products.Add(new Product { Id = _nextId++, Name = "Sample A", Category = "Category1", Price = 100 });
        _products.Add(new Product { Id = _nextId++, Name = "Sample B", Category = "Category2", Price = 200 });
    }

    public List<Product> GetAll()
    {
        if (_products.Count == 0)
        {
            throw new ProductExceptions.NoProductException();
        }
        return _products;
    }

    public List<Product> GetAllByCategory(string category)
    {
        if (_products.Count == 0)
            throw new ProductExceptions.NoProductException();

        var productsInCategory = _products
            .Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (productsInCategory.Count == 0)
            throw new ProductExceptions.NoProductWithCategory();

        return productsInCategory;
    }


    public Product? GetById(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            throw new ProductExceptions.NoProductException();
        }
        return product;
    }


    public Product Create(Product product)
    {
        if (_products.Any(p => p.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase)))
            throw new ProductExceptions.DuplicateNameException();

        if (product.Price < 0)
            throw new ProductExceptions.InvalidPriceException();

        product.Id = _nextId++;
        _products.Add(product);
        return product;
    }


    public Product Update(int id, Product updated)
    {
        var existing = _products.FirstOrDefault(p => p.Id == id);
        if (existing == null)
            throw new ProductExceptions.NoProductException();

        if (_products.Any(p => p.Id != id && p.Name.Equals(updated.Name, StringComparison.OrdinalIgnoreCase)))
            throw new ProductExceptions.DuplicateNameException();

        if (updated.Price < 0)
            throw new ProductExceptions.InvalidPriceException();

        existing.Name = updated.Name;
        existing.Category = updated.Category;
        existing.Price = updated.Price;

        return existing;
    }


    public Product Delete(int id)
    {
        var existing = _products.FirstOrDefault(p => p.Id == id);
        if (existing == null)
            throw new ProductExceptions.NoProductException();

        _products.Remove(existing);
        return existing;
    }
}

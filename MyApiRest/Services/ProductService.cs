using MyApiRest.Models;

namespace MyApiRest.Services;

public class ProductService //declaramos las operaciones crud de este servicio
{
    private readonly List<Product> _products = new();

    public IEnumerable<Product> GetAll() => _products;

    public Product? GetById(int id) => _products.FirstOrDefault(p => p.Id == id);

    public void Add(Product product)  //operacion crud para crear un producto
    {
        product.Id = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
        _products.Add(product);
    }

    public bool Update(int id, Product updatedProduct)  //operacion crud para actuacizar un producto por id
    {
        var product = GetById(id);
        if (product is null) return false;

        product.Name = updatedProduct.Name;
        product.Price = updatedProduct.Price;
        product.Stock = updatedProduct.Stock;
        return true;
    }

    public bool Delete(int id)  //operacion crud para borrar un producto por id
    {
        var product = GetById(id);
        if (product is null) return false;

        _products.Remove(product);
        return true;
    }
}

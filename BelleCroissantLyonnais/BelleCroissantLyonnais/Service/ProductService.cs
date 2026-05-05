using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BelleCroissantLyonnais.Models;
using BelleCroissantLyonnais.Repository;

namespace BelleCroissantLyonnais.Services
{
    public class ProductService
    {
        private readonly ProductRepository _repository;

        public ProductService()
        {
            _repository = new ProductRepository();
        }

        public List<Product> GetAllProducts()
        {
            return _repository.GetAll();
        }

        public void AddProduct(Product product)
        {
            var products = _repository.GetAll();
            product.Id = products.Count > 0 ? products.Max(p => p.Id) + 1 : 1;
            products.Add(product);
            _repository.Save(products);
        }

        public void UpdateProduct(Product product)
        {
            var products = _repository.GetAll();
            int index = products.FindIndex(p => p.Id == product.Id);
            if (index >= 0)
            {
                products[index] = product;
                _repository.Save(products);
            }
        }

        public void DeleteProduct(int id)
        {
            var products = _repository.GetAll();
            products.RemoveAll(p => p.Id == id);
            _repository.Save(products);
        }

        public List<Product> Search(string query)
        {
            var products = _repository.GetAll();
            if (string.IsNullOrWhiteSpace(query))
                return products;

            string q = query.ToLower();
            return products
                .Where(p => p.ProductName.ToLower().Contains(q) ||
                            p.Category.ToLower().Contains(q))
                .ToList();
        }
    }
}
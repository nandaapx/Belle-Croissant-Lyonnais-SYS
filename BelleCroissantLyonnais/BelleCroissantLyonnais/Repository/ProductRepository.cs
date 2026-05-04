using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BelleCroissantLyonnais.Models;
using Newtonsoft.Json;
using System.IO;

namespace BelleCroissantLyonnais.Repository
{
    public class ProductRepository
    {
        private readonly string _filePath;

        public ProductRepository()
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "products.json");
        }

        public List<Product> GetAll()
        {
            if (!File.Exists(_filePath))
                return new List<Product>();

            string json = File.ReadAllText(_filePath);

            if (string.IsNullOrWhiteSpace(json))
                return new List<Product>();

            return JsonConvert.DeserializeObject<List<Product>>(json);
        }

        public void Save(List<Product> products)
        {
            string json = JsonConvert.SerializeObject(products, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
    }
}

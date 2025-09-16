using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Beispiel: Lebensmittel Nährwerte abrufen (Coca Cola)");

        // Beispiel-Barcode: Coca Cola 500ml
        string barcode = "5449000000996";

        try
        {
            Product? product = await GetProductAsync(barcode);

            if (product != null)
            {
                Console.WriteLine($"Produkt: {product.Name}");
                Console.WriteLine($"Marke: {product.Brand}");
                Console.WriteLine($"Kalorien: {product.Calories} kcal");
                Console.WriteLine($"Eiweiß: {product.Protein} g");
                Console.WriteLine($"Fett: {product.Fat} g");
                Console.WriteLine($"Kohlenhydrate: {product.Carbs} g");
            }
            else
            {
                Console.WriteLine("Produkt nicht gefunden.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Fehler beim Abrufen: " + ex.Message);
        }
    }

    public static async Task<Product?> GetProductAsync(string barcode)
    {
        using var http = new HttpClient();
        string url = $"https://world.openfoodfacts.org/api/v0/product/{barcode}.json";

        var response = await http.GetAsync(url);
        if (!response.IsSuccessStatusCode) return null;

        using var stream = await response.Content.ReadAsStreamAsync();
        var json = await JsonDocument.ParseAsync(stream);

        if (json.RootElement.GetProperty("status").GetInt32() != 1)
            return null;

        var productElement = json.RootElement.GetProperty("product");

        // Nährwerte: manche Felder können fehlen -> TryGetDouble
        double GetValue(string key)
        {
            if (productElement.GetProperty("nutriments").TryGetProperty(key, out var val) &&
                val.TryGetDouble(out double d))
                return d;
            return 0;
        }

        return new Product
        {
            Name = productElement.GetProperty("product_name").GetString(),
            Brand = productElement.GetProperty("brands").GetString(),
            Calories = GetValue("energy-kcal_100g"),
            Protein = GetValue("proteins_100g"),
            Fat = GetValue("fat_100g"),
            Carbs = GetValue("carbohydrates_100g")
        };
    }

    public class Product
    {
        public string? Name { get; set; }
        public string? Brand { get; set; }
        public double Calories { get; set; }
        public double Protein { get; set; }
        public double Fat { get; set; }
        public double Carbs { get; set; }
    }
}

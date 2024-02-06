namespace GetFieldFromJson
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.Json;

    internal class Program
    {
        static void Main(string[] args) => Data.GetFieldFromJsonDoc();
    }

    public class City
    {
        public int? id { get; set; }
        public string? name { get; set; }
        public int? countryId { get; set; }
    }

    public class Country
    {
        public int? id { get; set; }
        public string? name { get; set; }
        public int? continentId { get; set; }
    }

    public class Address
    {
        public City city { get; set; } = new City();
        public Country country { get; set; } = new Country();
        public Continent continent { get; set; } = new Continent();
        public string? addressType { get; set; }
    }

    public class Continent
    {
        public int? id { get; set; }
    }

    public class Fields
    {
        public int? id { get; set; }
        public string? name { get; set; }
        public string? type { get; set; }
        public Address? address { get; set; }
        public string? availableGender { get; set; }
        public string? genders { get; set; }
        public List<string>? brandsLookOut { get; set; }
        public int? visible { get; set; }
        public string? createdDateTime { get; set; }
        public bool? isOwnedByTenant { get; set; }
    }

    public class Data
    {
        public List<Fields>? Fields { get; set; }

        public static void GetFieldFromJsonDoc()
        {
            var filePath = @"C:\Users\user\Desktop\Tests\get_all.json";

            try
            {
                var json = File.ReadAllText(filePath);
                var fields = JsonSerializer.Deserialize<List<Fields>>(json);

                if (fields == null)
                {
                    throw new JsonException("Failed to deserialize JSON data.");
                }

                var sortedData = fields
                        .OrderBy(field => field.id)
                        .ToList();

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                };

                var serializedJson = JsonSerializer.Serialize(sortedData, options);

                var outputFilePath = @"C:\Users\user\Desktop\Tests\off-rotation.json";
                File.WriteAllText(outputFilePath, serializedJson);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
            }
        }
    }
}

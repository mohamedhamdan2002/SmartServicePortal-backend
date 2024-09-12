using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json;
using System.Xml.XPath;

namespace Infrastructure.Data;

public static class DataInitializer
{
    private static readonly string _direcotry = Directory.GetCurrentDirectory();
    static DataInitializer()
    {
        var baseDirectory = Directory.GetParent(Directory.GetCurrentDirectory());
        if (baseDirectory is null)
            throw new InvalidOperationException();
        var currentProjectFolder = Path.Combine(baseDirectory.FullName, "Infrastructure");
        var targetDirecotry = Path.Combine(currentProjectFolder, "DataSeed");
        _direcotry = targetDirecotry;
    }
  
    public static async Task InitializeAsync(AppDbContext dbContext)
    {

        await SetData<Category>(dbContext, FilePath("categories.json"));
        await SetData<Service>(dbContext, FilePath("services.json"));

    }
    private static IEnumerable<T>? GetDataFromFile<T>(string path) where T : BaseEntity
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"File Not Found At: ${path}");
        var dataAsString = File.ReadAllText(path);
        var Data = JsonSerializer.Deserialize<IEnumerable<T>>(dataAsString);
        return Data;
    }

    private static async Task SetData<T>(AppDbContext dbContext, string path) where T : BaseEntity
    {
        if (dbContext.Set<T>().Any()) return;
        var data = GetDataFromFile<T>(path);
        if (data is null || !data.Any())
            throw new InvalidDataException("The data in the file is either null or empty.");
        dbContext.AddRange(data);
        await dbContext.SaveChangesAsync();
    }
    private static string FilePath(string fileName)
        => Path.Combine(_direcotry, fileName);
}

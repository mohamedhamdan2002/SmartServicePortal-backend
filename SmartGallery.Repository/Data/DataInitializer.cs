using SmartGallery.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmartGallery.Repository.Data
{
    public static class DataInitializer
    {
        private static readonly string _direcotry = "../SmartGallery.Repository/Data/DataSeed";
        public static async Task InitializeAsync(AppDbContext dbContext)
        {

            await SetData<Category>(dbContext, $"{_direcotry}/categories.json");
            await SetData<Service>(dbContext, $"{_direcotry}/services.json");

        }
        private static IEnumerable<T>? GetDataFromFile<T>(string path) where T : BaseEntity
        {
            var dataAsString = File.ReadAllText(path);
            var Data = JsonSerializer.Deserialize<IEnumerable<T>>(dataAsString);
            return Data;
        }

        private static async Task SetData<T>(AppDbContext dbContext, string path) where T : BaseEntity
        {
            if (!dbContext.Set<T>().Any())
            {
                var data = GetDataFromFile<T>(path);
                if (data?.Count() > 0)
                {
                    foreach (var item in data)
                        await dbContext.Set<T>().AddAsync(item);
                    await dbContext.SaveChangesAsync();
                }
            }

        }
    }
}

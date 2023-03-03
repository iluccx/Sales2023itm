using Sales.Shared.Entities;

namespace Sales.API.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context) {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();   //update database por codigo. Si no hay base de datos la crea, lo mismo con las migraciones
            await CheckCountriesAsync();
           // await CheckCategoriesAsync();
        }

        public async Task SeedCategoryAsync()
        {
            await _context.Database.EnsureCreatedAsync();  
            await CheckCategoriesAsync();
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country { Name = "Colombia"});
                _context.Countries.Add(new Country { Name = "Perú" });
                _context.Countries.Add(new Country { Name = "México" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Moto" });
                _context.Categories.Add(new Category { Name = "Carro" });
                _context.Categories.Add(new Category { Name = "Avion" });
                await _context.SaveChangesAsync();
            }
        }
    }
}

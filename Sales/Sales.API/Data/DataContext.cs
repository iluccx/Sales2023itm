using Microsoft.EntityFrameworkCore;
using Sales.Shared.Entities;

namespace Sales.API.Data
{
    public class DataContext : DbContext //Debo de heredar e instalar el entity framework. para conectarme a la db necesito constructor
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) //ctor constructor
        {

        }

        //para mapear mis tablas hay que crear una propiedad de tipo DbSet (Debo importar la clase)

        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<Category> Categories { get; set; }


        //Para validar duplicados es creando un indice unico

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(x => x.Name).IsUnique(); //se usa expresion lambda
            modelBuilder.Entity<Category>().HasIndex(x => x.Name).IsUnique(); //se usa expresion lambda
            modelBuilder.Entity<State>().HasIndex("CountryId","Name").IsUnique();
            modelBuilder.Entity<City>().HasIndex("StateId","Name").IsUnique();
        }

    }
}


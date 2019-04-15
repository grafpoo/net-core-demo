using Microsoft.EntityFrameworkCore;

namespace insignia.Models
{
    public class AirplaneDbContext : DbContext
    {
        public AirplaneDbContext()
        {
            //Database.SetInitializer<AirplaneDbContext>(new CreateDatabaseIfNotExists<AirplaneDbContext>());
            //: base("name=AirplaneDbContext")
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            CurrentEnvironment.CheckDBstructure();
            optionsBuilder.UseMySQL("server=localhost;database=airplane;user=root;password=password");
        }
        public virtual DbSet<Airplane> Airplanes { get; set; }

    }
}

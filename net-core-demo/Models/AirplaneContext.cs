using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace insignia.Models
{
    public class AirplaneContext : DbContext
    {
        public AirplaneContext(DbContextOptions<AirplaneContext> options) : base(options)
        {
            Database.ExecuteSqlCommand("drop table airplanes");
            Database.ExecuteSqlCommand("create table airplanes ( ID varchar(255), Name varchar(255) )");
            Database.ExecuteSqlCommand("insert into airplanes values ('ID22', 'Boeing-747')");
        }

        public DbSet<Airplane> Airplanes { get; set; }
    }

    //[Table("Airplane")]
    //public class AirplaneData
    //{
    //    [Key]
    //    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    //    public string Id { get; set; }

    //    public string Data { get; set; }
    //}
}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insignia.Models
{
    public class AirplaneContext : DbContext
    {
        public AirplaneContext(DbContextOptions<AirplaneContext> options) : base(options)
        {

        }

        public DbSet<Airplane> Airplanes { get; set; }
    }

    [Table("Airplane")]
    public class AirplaneData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        public string Data { get; set; }
    }
}

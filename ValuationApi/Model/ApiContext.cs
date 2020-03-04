using Microsoft.EntityFrameworkCore;

namespace ValuationApi.Model
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }
        
        public DbSet<Vessel> Vessels { get; set; }
        public DbSet<VesselType> VesselTypes { get; set; }
        public DbSet<TimeSeries> TimeSeries { get; set; }
        public DbSet<Valuation> Valuations { get; set; }        
    }
}

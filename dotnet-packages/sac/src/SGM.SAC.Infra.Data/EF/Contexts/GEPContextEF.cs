using Microsoft.EntityFrameworkCore;

namespace SGM.Core.Infra.Data.Contexts.EF
{
    public class GEPContextEF : DbContext
    {
        public GEPContextEF(DbContextOptions options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace SGM.SAC.Infra.Data.Contexts.EF
{
    public class SACContextEF : DbContext
    {
        public SACContextEF(DbContextOptions options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

        }
    }
}

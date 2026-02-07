using Microsoft.EntityFrameworkCore;

namespace Biashara_POS.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace SFA.DAS.WebTemplateSourceName.Data
{
    public class WebTemplateSourceNameDbContext : DbContext, IWebTemplateSourceNameDbContext
    {
        public WebTemplateSourceNameDbContext(DbContextOptions<WebTemplateSourceNameDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

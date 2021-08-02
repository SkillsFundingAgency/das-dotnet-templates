using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SFA.DAS.WebTemplateSourceName.Data
{
    public class DbContextFactory: IDbContextFactory<WebTemplateSourceNameDbContext>
    {
        private readonly SqlConnection _sqlConnection;
        private readonly ILoggerFactory _loggerFactory;
        private readonly AzureServiceTokenProvider _azureServiceTokenProvider;

        public DbContextFactory(SqlConnection sqlConnection, ILoggerFactory loggerFactory, AzureServiceTokenProvider azureServiceTokenProvider)
        {
            _azureServiceTokenProvider = azureServiceTokenProvider;
            _sqlConnection = sqlConnection;
            if (_azureServiceTokenProvider != null)
            {
                _sqlConnection.AccessToken = _azureServiceTokenProvider.GetAccessTokenAsync("https://database.windows.net/").GetAwaiter().GetResult();
            }
            _loggerFactory = loggerFactory;
        }

        public WebTemplateSourceNameDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<WebTemplateSourceNameDbContext>()
                .UseSqlServer(_sqlConnection)
                .UseLoggerFactory(_loggerFactory);

            var dbContext = new WebTemplateSourceNameDbContext(optionsBuilder.Options);

            return dbContext;
        }
    }
}

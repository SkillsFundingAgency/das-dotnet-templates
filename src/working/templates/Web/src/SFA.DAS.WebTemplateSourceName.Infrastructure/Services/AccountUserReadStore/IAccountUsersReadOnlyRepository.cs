using SFA.DAS.CosmosDb;
using SFA.DAS.WebTemplateSourceName.Infrastructure.Services.AccountUsersReadStore.Types;

namespace SFA.DAS.WebTemplateSourceName.Infrastructure.Services.AccountUsersReadStore
{
    public interface IAccountUsersReadOnlyRepository : IReadOnlyDocumentRepository<AccountUser>
    {
    }
}
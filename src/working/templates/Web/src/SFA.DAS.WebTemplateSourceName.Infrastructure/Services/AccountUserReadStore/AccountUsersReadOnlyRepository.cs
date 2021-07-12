using Microsoft.Azure.Documents;
using SFA.DAS.CosmosDb;
using SFA.DAS.WebTemplateSourceName.Infrastructure.Services.AccountUsersReadStore.Types;
using SFA.DAS.WebTemplateSourceName.Infrastructure.Services.CosmosDb;

namespace SFA.DAS.WebTemplateSourceName.Infrastructure.Services.AccountUsersReadStore
{
    public class AccountUsersReadOnlyRepository : DocumentRepository<AccountUser>, IAccountUsersReadOnlyRepository
    {
        public AccountUsersReadOnlyRepository(IDocumentClient documentClient)
            : base(documentClient, DocumentSettings.DatabaseName, DocumentSettings.AccountUsersCollectionName)
        {
        }

        public AccountUsersReadOnlyRepository(IDocumentClientFactory documentClientFactory)
            : base(documentClientFactory.CreateDocumentClient(), DocumentSettings.DatabaseName, DocumentSettings.AccountUsersCollectionName)
        {
        }
    }
}
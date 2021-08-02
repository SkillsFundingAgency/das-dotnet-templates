using Microsoft.Azure.Documents;

namespace SFA.DAS.WebTemplateSourceName.Infrastructure.Services.CosmosDb
{
    public interface IDocumentClientFactory
    {
        IDocumentClient CreateDocumentClient();
    }
}
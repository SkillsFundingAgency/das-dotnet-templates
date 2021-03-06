using System;
using SFA.DAS.CosmosDb;
using SFA.DAS.EmployerAccounts.Types.Models;

namespace SFA.DAS.WebTemplateSourceName.Infrastructure.Services.AccountUsersReadStore.Types
{
    public class AccountUser : IDocument
    {
        public Guid Id { get; set; }
        public string ETag { get; set; }
        public Guid userRef { get; set; }
        public long accountId { get; set; }
        public DateTime? removed { get; set; }
        public UserRole? role { get; set; }
    }
}
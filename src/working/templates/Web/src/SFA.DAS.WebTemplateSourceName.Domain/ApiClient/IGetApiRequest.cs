using System.Text.Json.Serialization;

namespace SFA.DAS.WebTemplateSourceName.Domain.ApiClient
{
    public interface IGetApiRequest : IBaseApiRequest
    {
        [JsonIgnore]
        string GetUrl { get; }
    }
}

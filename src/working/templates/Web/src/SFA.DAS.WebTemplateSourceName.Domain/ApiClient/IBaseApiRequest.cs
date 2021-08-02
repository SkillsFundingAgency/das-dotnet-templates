using System.Text.Json.Serialization;

namespace SFA.DAS.WebTemplateSourceName.Domain.ApiClient
{
    public interface IBaseApiRequest
    {
        [JsonIgnore]
        string BaseUrl { get; }
    }
}
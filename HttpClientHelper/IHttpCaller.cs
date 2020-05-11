using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientHelper
{
    public interface IHttpCaller
    {
        Task<HttpResponseMessage> CallFailApi();
    }
}
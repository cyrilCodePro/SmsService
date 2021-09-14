using System.Threading.Tasks;

namespace MessageComponets.ApiService
{
    public interface IApiSmsService
    {
        Task SendSmsToApi(object payload);
    }
}
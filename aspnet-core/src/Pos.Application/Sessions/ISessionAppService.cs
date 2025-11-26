using Abp.Application.Services;
using Pos.Sessions.Dto;
using System.Threading.Tasks;

namespace Pos.Sessions;

public interface ISessionAppService : IApplicationService
{
    Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
}

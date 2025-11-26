using Abp.Application.Services;
using Pos.MultiTenancy.Dto;

namespace Pos.MultiTenancy;

public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
{
}


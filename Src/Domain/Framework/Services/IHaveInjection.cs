using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Framework.Services;

public interface IHaveInjection
{
    void Inject(IServiceCollection collection, IConfiguration configuration);
}
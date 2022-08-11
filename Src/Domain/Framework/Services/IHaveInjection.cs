using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pumpkin.Domain.Framework.Services;

public interface IHaveInjection
{
    void Inject(IServiceCollection collection, IConfiguration configuration);
}
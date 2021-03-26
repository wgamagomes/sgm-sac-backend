using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace SGM.GEP.Infra.Crosscutting.Startup
{
    public interface IStartup
    {
        void ConfigureServices(IServiceCollection services, IConfiguration config);
        void ScheduleJobs(IScheduler scheduler);
    }
}

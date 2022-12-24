using Application.Services;
using Application.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IWorkerService, WorkerService>();
        collection.AddScoped<IAdminService, AdminService>();
        collection.AddScoped<IMessageService, MessageService>();
        collection.AddScoped<IReportService, ReportService>();
        return collection;
    }
}
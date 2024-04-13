using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Poc.Command.TryWhatsApp;
using Poc.Contract.Command.TryWhatsApp.Request;

namespace Poc.Command;

public class CommandInitializer
{
    public static void Initialize(IServiceCollection services)
    {
        services.AddTransient<IRequestHandler<CreateCalendarAlertCommand, Result>, CreateCalendarAlertHandler>();
        services.AddTransient<IRequestHandler<CreateCodeCommand, Result>, CreateCodeHandler>();
    }
}

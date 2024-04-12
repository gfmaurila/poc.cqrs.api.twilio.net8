using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Poc.Command.Notification;
using Poc.Contract.Command.Notification.Request;

namespace Poc.Command;

public class CommandInitializer
{
    public static void Initialize(IServiceCollection services)
    {
        #region Notificações
        services.AddTransient<IRequestHandler<CreateNotificationSMSCommand, Result>, CreateNotificationSMSCommandHandler>();
        services.AddTransient<IRequestHandler<CreateNotificationWhatsAppCommand, Result>, CreateNotificationWhatsAppCommandHandler>();
        services.AddTransient<IRequestHandler<CreateNotificationSendGridEmailCommand, Result>, CreateNotificationSendGridEmailCommandHandler>();
        #endregion
    }
}

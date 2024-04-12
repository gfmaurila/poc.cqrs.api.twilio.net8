using Ardalis.Result;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using poc.core.api.net8.Interface;
using Poc.Auth.Twilio.Interfaces;
using Poc.Contract.Command.TryWhatsApp.Request;

namespace Poc.Command.TryWhatsApp;

public class CreateCreateCalendarAlertHandler : IRequestHandler<CreateCalendarAlertCommand, Result>
{
    private readonly ILogger<CreateCreateCalendarAlertHandler> _logger;
    private readonly ITwilioService _twilioService;
    private readonly IRedisCacheService<CreateCalendarAlertCommand> _redis;
    private readonly IMapper _mapper;
    public CreateCreateCalendarAlertHandler(ILogger<CreateCreateCalendarAlertHandler> logger,
                                            ITwilioService twilioService,
                                            IRedisCacheService<CreateCalendarAlertCommand> redis,
                                            IMapper mapper)
    {
        _logger = logger;
        _twilioService = twilioService;
        _mapper = mapper;
        _redis = redis;
    }
    public async Task<Result> Handle(CreateCalendarAlertCommand request, CancellationToken cancellationToken)
    {
        const string cacheKey = nameof(CreateCalendarAlertCommand);
        await _redis.SetAsync(cacheKey, request);

        var response = await _twilioService.CalendarAlertAsync(request);

        return Result.SuccessWithMessage("Mensagem enviada com sucesso!");
    }
}

using Microsoft.Extensions.Logging;
using poc.core.api.net8.Interface;
using Poc.Auth.Twilio.Interfaces;
using Poc.Auth.Twilio.Mapper;
using Poc.Auth.Twilio.Response;
using Poc.Contract.Command.TryWhatsApp.Request;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Poc.Auth.Twilio.Services;

public class TwilioService : ITwilioService
{
    private readonly ILogger<TwilioService> _logger;
    private readonly IRedisCacheService<TwilioMessageResponse> _redis;

    public TwilioService(ILogger<TwilioService> logger,
                         IRedisCacheService<TwilioMessageResponse> redis)
    {
        _logger = logger;
        _redis = redis;
    }

    public async Task<TwilioMessageResponse> CalendarAlertAsync(CreateCalendarAlertCommand request)
    {
        TwilioClient.Init(request.Auth.AccountSid, request.Auth.AuthToken);

        var message = await MessageResource.CreateAsync(
            body: request.Body,
            from: new PhoneNumber(request.Auth.From),
            to: new PhoneNumber(request.To)
        );

        var response = TwilioMapper.MapTwilioMessageResponseToMessageResponse(message);

        if (message.SubresourceUris != null)
        {
            response.SubresourceUris = new SubresourceUrisResponse
            {
                Media = message.SubresourceUris.ToString()
            };
        }

        const string cacheKey = nameof(TwilioMessageResponse);
        await _redis.SetAsync("CalendarAlertAsync_" + cacheKey, response);

        return response;
    }
}

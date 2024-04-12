using Poc.Auth.Twilio.Response;
using Twilio.Rest.Api.V2010.Account;

namespace Poc.Auth.Twilio.Mapper;

public static class TwilioMapper
{
    public static TwilioMessageResponse MapTwilioMessageResponseToMessageResponse(MessageResource message)
    {
        var response = new TwilioMessageResponse
        {
            AccountSid = message.AccountSid,
            ApiVersion = message.ApiVersion,
            Body = message.Body,
            DateCreated = message.DateCreated ?? DateTime.Now,
            DateSent = message.DateSent,
            DateUpdated = message.DateUpdated ?? DateTime.Now,
            Direction = message.Direction.ToString(),
            ErrorCode = message.ErrorCode,
            ErrorMessage = message.ErrorMessage,
            From = message.From.ToString(),
            NumMedia = message.NumMedia,
            NumSegments = message.NumSegments,
            Price = message.Price?.ToString(),
            PriceUnit = message.PriceUnit,
            Sid = message.Sid,
            Status = message.Status.ToString(),
            To = message.To.ToString(),
            Uri = message.Uri
        };

        return response;
    }
}

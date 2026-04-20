using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Tahfeez.Application.Services;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Tahfeez.Infrastracture.Services;

/// <summary>
/// Sends WhatsApp messages via Twilio's WhatsApp sandbox / approved sender.
/// Configure in appsettings:
/// {
///   "Twilio": {
///     "AccountSid": "ACxxxxxxxx",
///     "AuthToken": "xxxxxxxx",
///     "WhatsAppFrom": "whatsapp:+14155238886"
///   }
/// }
/// </summary>
public sealed class TwilioWhatsAppService : IWhatsAppService
{
    private readonly string _fromNumber;
    private readonly ILogger<TwilioWhatsAppService> _logger;

    public TwilioWhatsAppService(IConfiguration config, ILogger<TwilioWhatsAppService> logger)
    {
        _logger = logger;

        var accountSid = config["Twilio:AccountSid"]
            ?? throw new InvalidOperationException("Twilio:AccountSid is not configured.");
        var authToken = config["Twilio:AuthToken"]
            ?? throw new InvalidOperationException("Twilio:AuthToken is not configured.");

        _fromNumber = config["Twilio:WhatsAppFrom"]
            ?? throw new InvalidOperationException("Twilio:WhatsAppFrom is not configured.");

        TwilioClient.Init(accountSid, authToken);
    }

    public async Task<bool> SendAsync(string toPhoneNumber, string message, CancellationToken cancellationToken = default)
    {
        try
        {
            var twilioMessage = await MessageResource.CreateAsync(
                to: new Twilio.Types.PhoneNumber($"whatsapp:{toPhoneNumber}"),
                from: new Twilio.Types.PhoneNumber(_fromNumber),
                body: message);

            _logger.LogInformation("WhatsApp sent to {To}: SID={Sid}", toPhoneNumber, twilioMessage.Sid);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send WhatsApp message to {To}", toPhoneNumber);
            return false;
        }
    }
}

namespace Tahfeez.Application.Services;

/// <summary>
/// Abstraction for sending WhatsApp messages via any provider (Twilio, etc.).
/// </summary>
public interface IWhatsAppService
{
    /// <summary>Send a plain-text WhatsApp message to a phone number.</summary>
    /// <param name="toPhoneNumber">E.164 format: e.g. "+966500000000"</param>
    /// <param name="message">The message body</param>
    Task<bool> SendAsync(string toPhoneNumber, string message, CancellationToken cancellationToken = default);
}

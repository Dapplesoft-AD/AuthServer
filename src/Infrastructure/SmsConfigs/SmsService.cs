using Application.Abstractions.SMS;
using Domain.SmsConfigs;
using Microsoft.Extensions.Logging;
using SharedKernel;

namespace Infrastructure.SmsConfigs;

public sealed class SmsService(
    ISmsConfigRepository configRepository,
    ILogger<SmsService> logger
) : ISmsService
{
    private static readonly HttpClient _httpClient = new();

    public async Task<Result> SendOtpAsync(
        string phoneNumber,
        string message,
        CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber) ||
            string.IsNullOrWhiteSpace(message))
        {
            return Result.Failure("Phone or message is empty.");
        }

        SmsConfig? config =
            await configRepository.GetActiveProviderAsync(ct);

        if (config is null)
        {
            return Result.Failure(Error.NotFound("SMS Service: 404", "No SMS config available"));
        }

        try
        {
            using var request = new HttpRequestMessage(
                HttpMethod.Post,
                config.ApiUrl);

            request.Content = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    { "token", config.Token },
                    { "to", phoneNumber },
                    { "message", message }
                });

            HttpResponseMessage response =
                await _httpClient.SendAsync(request, ct);

            string responseText =
                await response.Content.ReadAsStringAsync(ct);

            logger.LogInformation(
                "Sms send status for {Phone}: {Response}",
                phoneNumber,
                responseText);

            if (!response.IsSuccessStatusCode)
            {
                return Result.Failure(
                    $"HTTP error {response.StatusCode}: {responseText}");
            }

            if (!responseText.Contains("SUCCESS", StringComparison.OrdinalIgnoreCase))
            {
                return Result.Failure(
                    $"GreenWeb rejected SMS: {responseText}");
            }

            return Result.Success();
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Sms sending failed to {Phone}",
                phoneNumber);

            return Result.Failure<Guid>(ex.Message);
        }
    }
}

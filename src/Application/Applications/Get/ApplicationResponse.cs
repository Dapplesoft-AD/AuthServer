namespace Application.Applications.Get;
public sealed record ApplicationResponse(
    Guid Id,
    string Name,
    string ClientId,
    string ClientSecret,
    Uri RedirectUri,   
    Uri ApiBaseUrl,    
    int Status,
    string StatusName
);

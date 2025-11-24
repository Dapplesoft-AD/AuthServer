namespace Domain.Applications;

public sealed class Applicationapply 
{
    public Guid Id { get; set; } 

    public string Name { get; set; } = string.Empty; 

    public string ClientId { get; set; } = string.Empty;

    public string ClientSecret { get; set; } = string.Empty; 

    public string RedirectUri { get; set; } = string.Empty;

    public string ApiBaseUrl { get; set; } = string.Empty;

    public ApplicationStatus Status { get; set; }
    public enum ApplicationStatus
    {
        Active = 1,
        Inactive = 2
    }

}


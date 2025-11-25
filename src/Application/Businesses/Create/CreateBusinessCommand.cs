using Application.Abstractions.Messaging;
using Domain.Businesses;

namespace Application.Businesses.Create;

public class CreateBusinessCommand : ICommand<Guid>
{
    public Guid OwnerUserId { get; set; }
    public string BusinessName { get; set; }
    public string IndustryType { get; set; }
    public string LogoUrl { get; set; }
    public BusinessStatus Status { get; set; }
}


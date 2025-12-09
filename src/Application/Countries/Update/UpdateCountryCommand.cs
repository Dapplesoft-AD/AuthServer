using Application.Abstractions.Messaging;

namespace Application.Countries.Update;

public sealed class UpdateCountryCommand() : ICommand
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Capital { get; set; } = null!;
    public string PhoneCode { get; set; } = null!;
    public bool IsActive { get; set; }
}

using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Countries.Create;

public sealed class CreateCountryCommand : ICommand<Guid>
{
    public string Name { get; set; } = null!;
    public string Capital { get; set; } = null!;
    public string PhoneCode { get; set; } = null!;
    public bool IsActive { get; set; }
}


using SharedKernel;

namespace Domain.Countries;

public class Country : Entity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Capital { get; set; } = null!;
    public string PhoneCode { get; set; } = null!;
    public bool? IsNew { get; set; }
}


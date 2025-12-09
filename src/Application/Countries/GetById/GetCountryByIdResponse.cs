namespace Application.Countries.GetById;

public sealed class GetCountryByIdResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Capital { get; set; } = string.Empty;
    public string PhoneCode { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

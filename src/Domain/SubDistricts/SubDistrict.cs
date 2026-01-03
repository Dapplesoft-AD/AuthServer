namespace Domain.SubDistricts;

public class SubDistrict
{
    public Guid Id { get; set; }
    public Guid DistrictId { get; set; }
    public string Name { get; set; }
    public bool? IsNew { get; set; }
}

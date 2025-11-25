using SharedKernel;

namespace Domain.Roles;

public class Role : Entity
{
    public Guid Id { get; set; }
    public string RoleName { get; set; }
    public string Description { get; set; }
}

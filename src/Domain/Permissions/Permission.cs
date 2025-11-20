using SharedKernel;

namespace Domain.Permissions;
public sealed class Permission : Entity
{
    public Guid Id { get; set; } 

    public string Code { get; set; } = string.Empty;  

    public string Description { get; set; } = string.Empty;
}

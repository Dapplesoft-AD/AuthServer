namespace Domain.Roles;
public sealed class Role 
{
    public Guid Id { get; set; } 

    public string RoleName { get; set; } = string.Empty; 

    public string Description { get; set; } = string.Empty;
}

using SharedKernel;

namespace Domain.BusinessMembers;

public class BusinessMember : Entity
{
    public Guid Id { get; set; }
    public Guid BusinessId { get; set; }
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public DateTime JoinedAt { get; set; }
}

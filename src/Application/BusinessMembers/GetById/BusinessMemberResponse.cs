namespace Application.BusinessMembers.GetById;

public sealed class BusinessMemberResponse
{
    public Guid Id { get; set; }
    public Guid BusinessId { get; set; }
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public DateTime JoinedAt { get; set; }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel;

namespace Domain.PasswordResets;

public class PasswordReset : Entity
{
    public Guid PR_Id { get; set; }
    public Guid User_Id { get; set; }
    public string Token { get; set; }  
    public DateTime Expires_at { get; set; }
    public bool Used { get; set; }
}

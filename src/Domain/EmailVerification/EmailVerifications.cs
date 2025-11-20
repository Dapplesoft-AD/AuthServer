using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel;

namespace Domain.EmailVerification;

public class EmailVerifications : Entity
{
    public Guid EV_Id { get; set; }
    public Guid User_Id { get; set; }
    public string Token { get; set; }  
    public DateTime Expires_at { get; set; }
    public DateTime Verified_at { get; set; }    
}

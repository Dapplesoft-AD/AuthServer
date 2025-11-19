using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Businesses;

namespace Application.Businesses.Get;
public sealed class BusinessResponse
{
    public Guid Id { get; set; }
    public Guid OwnerUserId { get; set; }
    public string BusinessName { get; set; }
    public string IndustryType { get; set; }
    public string LogoUrl { get; set; } 
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; }
}

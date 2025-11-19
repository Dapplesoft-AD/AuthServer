using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using SharedKernel;
using Domain.Enums;

namespace Domain.Application;
public class Applications : Entity
{
    public Guid Id {get; set; }
    public string Name { get; set; }
    public string Client_id { get; set; }   = string.Empty;
    public string Client_secret { get; set; }
    public string Redirect_url { get; set; }
    public string Api_base_url { get; set; }
    public Status Application_status { get; set; }
}

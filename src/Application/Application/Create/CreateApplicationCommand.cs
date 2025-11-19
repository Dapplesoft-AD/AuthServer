using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Application.Abstractions.Messaging;
using Domain.Enums;

namespace Application.Application.Create;

public sealed class CreateApplicationCommand : ICommand<Guid>
{
    public string Name { get; set; }
    public string Client_id { get; set; } = string.Empty;
    public string Client_secret { get; set; }
    public string Redirect_url { get; set; }
    public string Api_base_url { get; set; }
    public Status Application_status { get; set; }
}

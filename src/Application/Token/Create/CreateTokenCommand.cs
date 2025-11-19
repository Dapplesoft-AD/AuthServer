using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.Token.Create;

public sealed class CreateTokenCommand : ICommand<Guid>
{
    public Guid User_id { get; set; }
    public Guid App_id { get; set; }
    public string Acess_token { get; set; } //text
    public string Refresh_token { get; set; } // text
    public DateTime Issued_at { get; set; }
}

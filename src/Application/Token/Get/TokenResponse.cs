using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Token.Get;

public sealed class TokenResponse
{
    public Guid TokenId { get; set; }
    public Guid User_id { get; set; }
    public Guid App_id { get; set; }
    public string Access_token { get; set; } //text
    public string Refresh_token { get; set; } // text
    public DateTime Issued_at { get; set; }
}

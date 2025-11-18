using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Applications.Get;
public sealed record ApplicationResponse(
  Guid Id,
    string Name,
    string ClientId,
    string ClientSecret,
    Uri RedirectUri,    // Change from Uri to string
    Uri ApiBaseUrl,     // Change from Uri to string
    int Status,
    string StatusName
);

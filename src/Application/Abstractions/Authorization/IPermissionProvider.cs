using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Authorization;
public interface IPermissionProvider
{
    Task<HashSet<string>> GetForUserIdAsync(Guid userId);
}

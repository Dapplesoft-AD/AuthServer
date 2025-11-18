<<<<<<< HEAD
﻿using Domain.UserRoles;
using SharedKernel;
=======
﻿using SharedKernel;
>>>>>>> 8be9632798b68d2f9b5ec678c438c63cb1b8eb79

namespace Domain.Users;

public sealed class User : Entity
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PasswordHash { get; set; }
<<<<<<< HEAD
    public List<UserRole> UserRoles { get; set; } = []; // Add this line
=======
>>>>>>> 8be9632798b68d2f9b5ec678c438c63cb1b8eb79
}

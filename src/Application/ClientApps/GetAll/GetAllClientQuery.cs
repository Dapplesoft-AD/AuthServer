using System;
using System.Collections.Generic;
using System.Text;
using Application.Abstractions.Messaging;
using Application.Abstractions.Openiddict;
using SharedKernel.Models;

namespace Application.ClientApps.GetAll;

public sealed class GetAllClientQuery : IQuery<IReadOnlyList<ClientView>>;

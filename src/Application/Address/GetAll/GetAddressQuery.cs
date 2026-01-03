using Application.Abstractions.Messaging;

namespace Application.Address.GetAll;

public sealed class GetAddressQuery : IQuery<List<GetAddressQueryResponse>>;

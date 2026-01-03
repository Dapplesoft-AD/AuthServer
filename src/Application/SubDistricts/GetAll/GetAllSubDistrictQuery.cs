using Application.Abstractions.Messaging;
using Application.SubDistricts.Get;

namespace Application.SubDistricts.GetAll;

public sealed record GetAllSubDistrictQuery() : IQuery<List<SubDistrictResponse>>;

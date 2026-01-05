using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Database.Seed.LocationsSeed;

internal static class SeedIds
{
    internal static Guid For(string key)
    {
        byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(key.Trim().ToLower(System.Globalization.CultureInfo.CurrentCulture)));
        Span<byte> guidBytes = stackalloc byte[16];
        hash.AsSpan(0, 16).CopyTo(guidBytes);
        return new Guid(guidBytes);
    }

    internal static Guid Country(string name) => For($"country:{name}");
    internal static Guid Region(string country, string region) => For($"region:{country}:{region}");
    internal static Guid District(string country, string region, string district) => For($"district:{country}:{region}:{district}");
    internal static Guid SubDistrict(string country, string region, string district, string subDistrict ) => For($"district:{country}:{region}:{district}:{subDistrict}");
}

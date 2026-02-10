using Pure.HashCodes.Abstractions;

namespace Pure.RelationalSchema.Random.Tests;

internal sealed record DeterminedHashEqualityComparer : IEqualityComparer<IDeterminedHash>
{
    public bool Equals(IDeterminedHash? x, IDeterminedHash? y)
    {
        return x!.SequenceEqual(y!);
    }

    public int GetHashCode(IDeterminedHash obj)
    {
        HashCode hash = new HashCode();
        hash.AddBytes(obj.ToArray());
        return hash.ToHashCode();
    }
}

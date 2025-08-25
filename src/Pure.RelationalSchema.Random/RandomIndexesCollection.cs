using System.Collections;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Cached.Number;
using Pure.Primitives.Random.Number;
using Pure.RelationalSchema.Abstractions.Index;

namespace Pure.RelationalSchema.Random;

public sealed record RandomIndexesCollection : IEnumerable<IIndex>
{
    private readonly INumber<ushort> _count;

    private readonly System.Random _random;

    public RandomIndexesCollection()
        : this(new System.Random()) { }

    public RandomIndexesCollection(System.Random random)
        : this(new RandomUShort(random), random) { }

    public RandomIndexesCollection(INumber<ushort> count)
        : this(count, new System.Random()) { }

    public RandomIndexesCollection(INumber<ushort> count, System.Random random)
    {
        _count = count;
        _random = random;
    }

    public IEnumerator<IIndex> GetEnumerator()
    {
        for (int i = 0; i < _count.NumberValue; i++)
        {
            yield return new RandomIndex(_random);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}

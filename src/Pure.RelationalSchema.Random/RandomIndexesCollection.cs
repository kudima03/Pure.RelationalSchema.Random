using System.Collections;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Random.Number;
using Pure.RelationalSchema.Abstractions.Index;

namespace Pure.RelationalSchema.Random;

using Random = System.Random;

public sealed record RandomIndexesCollection : IEnumerable<IIndex>
{
    private readonly INumber<ushort> _count;

    private readonly RandomColumnsCollection _columns;

    private readonly Random _random;

    public RandomIndexesCollection()
        : this(Random.Shared) { }

    public RandomIndexesCollection(Random random)
        : this(new RandomUShort(random), random) { }

    public RandomIndexesCollection(INumber<ushort> count)
        : this(count, Random.Shared) { }

    public RandomIndexesCollection(INumber<ushort> count, Random random)
        : this(count, new RandomColumnsCollection(random), random) { }

    public RandomIndexesCollection(INumber<ushort> count, RandomColumnsCollection columns)
        : this(count, columns, Random.Shared) { }

    public RandomIndexesCollection(
        INumber<ushort> count,
        RandomColumnsCollection columns,
        Random random
    )
    {
        _count = count;
        _columns = columns;
        _random = random;
    }

    public IEnumerator<IIndex> GetEnumerator()
    {
        for (int i = 0; i < _count.NumberValue; i++)
        {
            yield return new RandomIndex(_columns, _random);
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

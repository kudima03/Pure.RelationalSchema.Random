using System.Collections;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Abstractions.String;
using Pure.Primitives.Random.Number;
using Pure.Primitives.Random.String;
using Pure.RelationalSchema.Abstractions.ColumnType;

namespace Pure.RelationalSchema.Random;

using Random = System.Random;

public sealed record RandomColumnTypesCollection : IEnumerable<IColumnType>
{
    private readonly INumber<ushort> _count;

    private readonly IEnumerable<IString> _names;

    public RandomColumnTypesCollection()
        : this(Random.Shared) { }

    public RandomColumnTypesCollection(Random random)
        : this(new RandomUShort(random), random) { }

    public RandomColumnTypesCollection(INumber<ushort> count)
        : this(count, Random.Shared) { }

    public RandomColumnTypesCollection(INumber<ushort> count, Random random)
        : this(count, new RandomStringCollection(random)) { }

    public RandomColumnTypesCollection(RandomStringCollection randomNames)
        : this(randomNames, Random.Shared) { }

    public RandomColumnTypesCollection(RandomStringCollection randomNames, Random random)
        : this(new RandomUShort(random), randomNames) { }

    public RandomColumnTypesCollection(
        INumber<ushort> count,
        RandomStringCollection randomNames
    )
        // Stryker disable once linq
        : this(count, randomNames.AsEnumerable()) { }

    private RandomColumnTypesCollection(INumber<ushort> count, IEnumerable<IString> names)
    {
        _count = count;
        _names = names;
    }

    public IEnumerator<IColumnType> GetEnumerator()
    {
        using IEnumerator<IString> namesEnumerator = _names.GetEnumerator();
        for (int i = 0; i < _count.NumberValue; i++)
        {
            yield return !namesEnumerator.MoveNext()
                ? throw new ArgumentException()
                : new RandomColumnType(namesEnumerator.Current);
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

using System.Collections;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Random.Number;
using Pure.RelationalSchema.Abstractions.ColumnType;

namespace Pure.RelationalSchema.Random;

using Random = System.Random;

public sealed record RandomColumnTypesCollection : IEnumerable<IColumnType>
{
    private readonly INumber<ushort> _count;

    private readonly Random _random;

    public RandomColumnTypesCollection()
        : this(new Random()) { }

    public RandomColumnTypesCollection(Random random)
        : this(new RandomUShort(random), random) { }

    public RandomColumnTypesCollection(INumber<ushort> count)
        : this(count, new Random()) { }

    public RandomColumnTypesCollection(INumber<ushort> count, Random random)
    {
        _count = count;
        _random = random;
    }

    public IEnumerator<IColumnType> GetEnumerator()
    {
        for (int i = 0; i < _count.NumberValue; i++)
        {
            yield return new RandomColumnType(_random);
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

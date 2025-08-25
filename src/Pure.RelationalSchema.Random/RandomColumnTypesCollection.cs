using System.Collections;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Cached.Number;
using Pure.Primitives.Number;
using Pure.Primitives.Random.Number;
using Pure.RelationalSchema.Abstractions.ColumnType;

namespace Pure.RelationalSchema.Random;

public sealed record RandomColumnTypesCollection : IEnumerable<IColumnType>
{
    private readonly INumber<ushort> _count;

    private readonly System.Random _random;

    public RandomColumnTypesCollection()
        : this(new System.Random()) { }

    public RandomColumnTypesCollection(System.Random random)
        : this(new CachedNumber<ushort>(new RandomUShort(new MinUshort(), new UShort(100), random)), random) { }

    public RandomColumnTypesCollection(INumber<ushort> count)
        : this(count, new System.Random()) { }

    public RandomColumnTypesCollection(INumber<ushort> count, System.Random random)
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

using System.Collections;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Number;
using Pure.Primitives.Random.Number;
using Pure.Primitives.Random.String;
using Pure.RelationalSchema.Abstractions.ColumnType;

namespace Pure.RelationalSchema.Random;

using Random = System.Random;

public sealed record RandomColumnTypesCollection : IEnumerable<IColumnType>
{
    private readonly INumber<ushort> _count;

    private readonly Random _random;

    public RandomColumnTypesCollection()
        : this(Random.Shared) { }

    public RandomColumnTypesCollection(Random random)
        : this(new RandomUShort(random), random) { }

    public RandomColumnTypesCollection(INumber<ushort> count)
        : this(count, Random.Shared) { }

    public RandomColumnTypesCollection(INumber<ushort> count, Random random)
    {
        _count = count;
        _random = random;
    }

    public IEnumerator<IColumnType> GetEnumerator()
    {
        for (int i = 0; i < _count.NumberValue; i++)
        {
            yield return new RandomColumnType(
                new RandomString(
                    new RandomUShort(new MinUshort(), new UShort(256)),
                    _random
                )
            );
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

using System.Collections;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Number;
using Pure.Primitives.Random.Number;
using Pure.Primitives.Random.String;
using Pure.RelationalSchema.Abstractions.Column;

namespace Pure.RelationalSchema.Random;

using Random = System.Random;

public sealed record RandomColumnsCollection : IEnumerable<IColumn>
{
    private readonly INumber<ushort> _count;

    private readonly Random _random;

    public RandomColumnsCollection()
        : this(Random.Shared) { }

    public RandomColumnsCollection(Random random)
        : this(new RandomUShort(random), random) { }

    public RandomColumnsCollection(INumber<ushort> count)
        : this(count, Random.Shared) { }

    public RandomColumnsCollection(INumber<ushort> count, Random random)
    {
        _count = count;
        _random = random;
    }

    public IEnumerator<IColumn> GetEnumerator()
    {
        for (int i = 0; i < _count.NumberValue; i++)
        {
            yield return new RandomColumn(
                new RandomString(
                    new RandomUShort(new MinUshort(), new UShort(256), _random),
                    _random
                ),
                new RandomColumnType(
                    new RandomString(
                        new RandomUShort(new MinUshort(), new UShort(256), _random),
                        _random
                    )
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

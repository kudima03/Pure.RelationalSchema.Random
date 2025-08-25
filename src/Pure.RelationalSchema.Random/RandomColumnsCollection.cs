using System.Collections;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Cached.Number;
using Pure.Primitives.Random.Number;
using Pure.RelationalSchema.Abstractions.Column;

namespace Pure.RelationalSchema.Random;

public sealed record RandomColumnsCollection : IEnumerable<IColumn>
{
    private readonly INumber<ushort> _count;

    private readonly System.Random _random;

    public RandomColumnsCollection()
        : this(new System.Random()) { }

    public RandomColumnsCollection(System.Random random)
        : this(new RandomUShort(random), random) { }

    public RandomColumnsCollection(INumber<ushort> count)
        : this(count, new System.Random()) { }

    public RandomColumnsCollection(INumber<ushort> count, System.Random random)
    {
        _count = count;
        _random = random;
    }

    public IEnumerator<IColumn> GetEnumerator()
    {
        for (int i = 0; i < _count.NumberValue; i++)
        {
            yield return new RandomColumn(_random);
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

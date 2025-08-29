using System.Collections;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Number;
using Pure.Primitives.Random.Bool;
using Pure.Primitives.Random.Number;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.Abstractions.Index;

namespace Pure.RelationalSchema.Random;

using Random = System.Random;

public sealed record RandomIndexesCollection : IEnumerable<IIndex>
{
    private readonly INumber<ushort> _count;

    private readonly IEnumerable<IEnumerable<IColumn>> _columns;

    private readonly Random _random;

    public RandomIndexesCollection()
        : this(Random.Shared) { }

    public RandomIndexesCollection(Random random)
        : this(new RandomUShort(new UShort(1), new UShort(10), random), random) { }

    public RandomIndexesCollection(INumber<ushort> count)
        : this(count, Random.Shared) { }

    public RandomIndexesCollection(INumber<ushort> count, Random random)
        : this(
            count,
            Enumerable
                .Range(0, count.NumberValue)
                .Select(_ => new RandomColumnsCollection(random)),
            random
        )
    { }

    public RandomIndexesCollection(
        INumber<ushort> count,
        IEnumerable<RandomColumnsCollection> columns
    )
        : this(count, columns, Random.Shared) { }

    public RandomIndexesCollection(
        INumber<ushort> count,
        IEnumerable<RandomColumnsCollection> columns,
        Random random
    )
        // Stryker disable once linq
        : this(count, columns.Cast<IEnumerable<IColumn>>().AsEnumerable(), random) { }

    private RandomIndexesCollection(
        INumber<ushort> count,
        IEnumerable<IEnumerable<IColumn>> columns,
        Random random
    )
    {
        _count = count;
        _columns = columns;
        _random = random;
    }

    public IEnumerator<IIndex> GetEnumerator()
    {
        using IEnumerator<IEnumerable<IColumn>> columnsEnumerator =
            _columns.GetEnumerator();
        for (int i = 0; i < _count.NumberValue; i++)
        {
            yield return !columnsEnumerator.MoveNext()
                ? throw new ArgumentException()
                : new RandomIndex(new RandomBool(_random), columnsEnumerator.Current);
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

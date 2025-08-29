using System.Collections;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Abstractions.String;
using Pure.Primitives.Number;
using Pure.Primitives.Random.Number;
using Pure.Primitives.Random.String;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.Abstractions.ColumnType;

namespace Pure.RelationalSchema.Random;

using Random = System.Random;

public sealed record RandomColumnsCollection : IEnumerable<IColumn>
{
    private readonly INumber<ushort> _count;

    private readonly IEnumerable<IString> _names;

    private readonly IEnumerable<IColumnType> _columnTypes;

    public RandomColumnsCollection()
        : this(Random.Shared) { }

    public RandomColumnsCollection(Random random)
        : this(new RandomUShort(new MinUshort(), new UShort(10), random), random) { }

    public RandomColumnsCollection(INumber<ushort> count)
        : this(count, Random.Shared) { }

    public RandomColumnsCollection(INumber<ushort> count, Random random)
        : this(
            count,
            new RandomStringCollection(
                count,
                new RandomUShortCollection(count, new MinUshort(), new UShort(100), random),
                random
            ),
            new RandomColumnTypesCollection(count, random)
        )
    { }

    public RandomColumnsCollection(
        INumber<ushort> count,
        RandomStringCollection randomNames,
        RandomColumnTypesCollection randomColumnTypes
    )
        // Stryker disable once linq
        : this(count, randomNames.AsEnumerable(), randomColumnTypes.AsEnumerable()) { }

    private RandomColumnsCollection(
        INumber<ushort> count,
        IEnumerable<IString> names,
        IEnumerable<IColumnType> columnTypes
    )
    {
        _count = count;
        _names = names;
        _columnTypes = columnTypes;
    }

    public IEnumerator<IColumn> GetEnumerator()
    {
        using IEnumerator<IString> namesEnumerator = _names.GetEnumerator();
        using IEnumerator<IColumnType> columnTypesEnumerator =
            _columnTypes.GetEnumerator();
        for (int i = 0; i < _count.NumberValue; i++)
        {
            yield return !namesEnumerator.MoveNext() || !columnTypesEnumerator.MoveNext()
                ? throw new ArgumentException()
                : new RandomColumn(
                    namesEnumerator.Current,
                    columnTypesEnumerator.Current
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

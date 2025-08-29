using System.Collections;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Abstractions.String;
using Pure.Primitives.Number;
using Pure.Primitives.Random.Number;
using Pure.Primitives.Random.String;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.Abstractions.Index;
using Pure.RelationalSchema.Abstractions.Table;

namespace Pure.RelationalSchema.Random;

using Random = System.Random;

public sealed record RandomTablesCollection : IEnumerable<ITable>
{
    private readonly INumber<ushort> _count;

    private readonly IEnumerable<IString> _name;

    private readonly IEnumerable<IEnumerable<IColumn>> _columns;

    private readonly IEnumerable<IEnumerable<IIndex>> _indexes;

    public RandomTablesCollection()
        : this(Random.Shared) { }

    public RandomTablesCollection(Random random)
        : this(new RandomUShort(new UShort(1), new UShort(10), random), random) { }

    public RandomTablesCollection(INumber<ushort> count)
        : this(count, Random.Shared) { }

    public RandomTablesCollection(INumber<ushort> count, Random random)
        : this(
            count,
            Enumerable
                .Range(0, count.NumberValue)
                .Select(_ => new RandomColumnsCollection(random)),
            Enumerable
                .Range(0, count.NumberValue)
                .Select(_ => new RandomIndexesCollection(random)),
            random
        )
    { }

    public RandomTablesCollection(
        INumber<ushort> count,
        IEnumerable<RandomColumnsCollection> columns,
        IEnumerable<RandomIndexesCollection> indexes
    )
        : this(count, columns, indexes, Random.Shared) { }

    public RandomTablesCollection(
        INumber<ushort> count,
        IEnumerable<RandomColumnsCollection> columns,
        IEnumerable<RandomIndexesCollection> indexes,
        Random random
    )
        : this(
            count,
            Enumerable
                .Range(0, count.NumberValue)
                .Select(_ => new RandomString(
                    new RandomUShort(new UShort(1), new UShort(10), random),
                    random
                )),
            columns,
            indexes
        )
    { }

    public RandomTablesCollection(
        INumber<ushort> count,
        RandomStringCollection randomNames,
        IEnumerable<RandomColumnsCollection> randomColumns,
        IEnumerable<RandomIndexesCollection> randomIndexes
    )
        // Stryker disable once linq
        : this(
            count,
            randomNames.AsEnumerable(),
            randomColumns.AsEnumerable(),
            randomIndexes.AsEnumerable()
        )
    { }

    private RandomTablesCollection(
        INumber<ushort> count,
        IEnumerable<IString> name,
        IEnumerable<IEnumerable<IColumn>> columns,
        IEnumerable<IEnumerable<IIndex>> indexes
    )
    {
        _count = count;
        _name = name;
        _columns = columns;
        _indexes = indexes;
    }

    public IEnumerator<ITable> GetEnumerator()
    {
        using IEnumerator<IString> namesEnumerator = _name.GetEnumerator();
        using IEnumerator<IEnumerable<IColumn>> columnsEnumerator =
            _columns.GetEnumerator();
        using IEnumerator<IEnumerable<IIndex>> indexesEnumerator =
            _indexes.GetEnumerator();
        for (int i = 0; i < _count.NumberValue; i++)
        {
            yield return !namesEnumerator.MoveNext()
            || !columnsEnumerator.MoveNext()
            || !indexesEnumerator.MoveNext()
                ? throw new ArgumentException()
                : new RandomTable(
                    namesEnumerator.Current,
                    columnsEnumerator.Current,
                    indexesEnumerator.Current
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

using System.Collections;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Number;
using Pure.Primitives.Random.Number;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.Abstractions.ForeignKey;
using Pure.RelationalSchema.Abstractions.Table;

namespace Pure.RelationalSchema.Random;

using Random = System.Random;

public sealed record RandomForeignKeysCollection : IEnumerable<IForeignKey>
{
    private readonly INumber<ushort> _count;

    private readonly IEnumerable<ITable> _referencingTables;

    private readonly IEnumerable<IColumn> _referencingColumns;

    private readonly IEnumerable<ITable> _referencedTables;

    private readonly IEnumerable<IColumn> _referencedColumns;

    public RandomForeignKeysCollection()
        : this(Random.Shared) { }

    public RandomForeignKeysCollection(Random random)
        : this(new RandomUShort(new MinUshort(), new UShort(10), random), random) { }

    public RandomForeignKeysCollection(INumber<ushort> count)
        : this(count, Random.Shared) { }

    public RandomForeignKeysCollection(INumber<ushort> count, Random random)
        : this(
            count,
            new RandomTablesCollection(count, random),
            new RandomColumnsCollection(count, random),
            new RandomTablesCollection(count, random),
            new RandomColumnsCollection(count, random)
        )
    { }

    public RandomForeignKeysCollection(
        INumber<ushort> count,
        RandomTablesCollection randomReferencingTables,
        RandomColumnsCollection randomReferencingColumns,
        RandomTablesCollection randomReferencedTables,
        RandomColumnsCollection randomReferencedColumns
    )
        // Stryker disable once linq
        : this(
            count,
            randomReferencingTables.AsEnumerable(),
            randomReferencingColumns.AsEnumerable(),
            randomReferencedTables.AsEnumerable(),
            randomReferencedColumns.AsEnumerable()
        )
    { }

    private RandomForeignKeysCollection(
        INumber<ushort> count,
        IEnumerable<ITable> referencingTables,
        IEnumerable<IColumn> referencingColumns,
        IEnumerable<ITable> referencedTables,
        IEnumerable<IColumn> referencedColumns
    )
    {
        _count = count;
        _referencingTables = referencingTables;
        _referencingColumns = referencingColumns;
        _referencedTables = referencedTables;
        _referencedColumns = referencedColumns;
    }

    public IEnumerator<IForeignKey> GetEnumerator()
    {
        using IEnumerator<ITable> referencingTablesEnumerator =
            _referencingTables.GetEnumerator();

        using IEnumerator<IColumn> referencingColumnsEnumerator =
            _referencingColumns.GetEnumerator();

        using IEnumerator<ITable> referencedTablesEnumerator =
            _referencedTables.GetEnumerator();

        using IEnumerator<IColumn> referencedColumnsEnumerator =
            _referencedColumns.GetEnumerator();

        for (int i = 0; i < _count.NumberValue; i++)
        {
            yield return !referencingTablesEnumerator.MoveNext()
            || !referencingColumnsEnumerator.MoveNext()
            || !referencedTablesEnumerator.MoveNext()
            || !referencedColumnsEnumerator.MoveNext()
                ? throw new ArgumentException()
                : new RandomForeignKey(
                    referencingTablesEnumerator.Current,
                    referencingColumnsEnumerator.Current,
                    referencedTablesEnumerator.Current,
                    referencedColumnsEnumerator.Current
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

using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.Abstractions.ForeignKey;
using Pure.RelationalSchema.Abstractions.Table;

namespace Pure.RelationalSchema.Random;

using Random = System.Random;

public sealed record RandomForeignKey : IForeignKey
{
    private readonly Lazy<IEnumerable<IColumn>> _lazyReferencingColumns;

    private readonly Lazy<IEnumerable<IColumn>> _lazyReferencedColumns;

    public RandomForeignKey()
        : this(Random.Shared) { }

    public RandomForeignKey(Random random)
        : this(
            new RandomTable(random),
            new RandomColumnsCollection(random),
            new RandomTable(random),
            new RandomColumnsCollection(random)
        )
    { }

    public RandomForeignKey(
        RandomTable randomReferencingTable,
        RandomColumnsCollection randomReferencingColumns,
        RandomTable randomReferencedTable,
        RandomColumnsCollection randomReferencedColumns
    )
        : this(
            randomReferencingTable,
            randomReferencingColumns.AsEnumerable(),
            randomReferencedTable,
            randomReferencedColumns.AsEnumerable()
        )
    { }

    internal RandomForeignKey(
        ITable referencingTable,
        IEnumerable<IColumn> referencingColumns,
        ITable referencedTable,
        IEnumerable<IColumn> referencedColumns
    )
        : this(
            referencingTable,
            new Lazy<IEnumerable<IColumn>>(referencingColumns.ToArray),
            referencedTable,
            new Lazy<IEnumerable<IColumn>>(referencedColumns.ToArray)
        )
    { }

    internal RandomForeignKey(
        ITable referencingTable,
        Lazy<IEnumerable<IColumn>> referencingColumns,
        ITable referencedTable,
        Lazy<IEnumerable<IColumn>> referencedColumns
    )
    {
        ReferencingTable = referencingTable;
        _lazyReferencingColumns = referencingColumns;
        ReferencedTable = referencedTable;
        _lazyReferencedColumns = referencedColumns;
    }

    public ITable ReferencingTable { get; }

    public IEnumerable<IColumn> ReferencingColumns => _lazyReferencingColumns.Value;

    public ITable ReferencedTable { get; }

    public IEnumerable<IColumn> ReferencedColumns => _lazyReferencedColumns.Value;

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}

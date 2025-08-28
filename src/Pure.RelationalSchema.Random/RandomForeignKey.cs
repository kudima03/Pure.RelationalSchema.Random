using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.Abstractions.ForeignKey;
using Pure.RelationalSchema.Abstractions.Table;

namespace Pure.RelationalSchema.Random;

using Random = System.Random;

public sealed record RandomForeignKey : IForeignKey
{
    public RandomForeignKey()
        : this(Random.Shared) { }

    public RandomForeignKey(Random random)
        : this(
            new RandomTable(random),
            new RandomColumn(random),
            new RandomTable(random),
            new RandomColumn(random)
        )
    { }

    public RandomForeignKey(
        RandomTable randomReferencingTable,
        RandomColumn randomReferencingColumn,
        RandomTable randomReferencedTable,
        RandomColumn randomReferencedColumn
    )
        : this(
            (ITable)randomReferencingTable,
            randomReferencingColumn,
            randomReferencedTable,
            randomReferencedColumn
        )
    { }

    internal RandomForeignKey(
        ITable referencingTable,
        IColumn referencingColumn,
        ITable referencedTable,
        IColumn referencedColumn
    )
    {
        ReferencingTable = referencingTable;
        ReferencingColumn = referencingColumn;
        ReferencedTable = referencedTable;
        ReferencedColumn = referencedColumn;
    }

    public ITable ReferencingTable { get; }

    public IColumn ReferencingColumn { get; }

    public ITable ReferencedTable { get; }

    public IColumn ReferencedColumn { get; }

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}

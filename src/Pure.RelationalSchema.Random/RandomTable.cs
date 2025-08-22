using Pure.Primitives.Abstractions.String;
using Pure.Primitives.Cached.String;
using Pure.Primitives.Random.Number;
using Pure.Primitives.Random.String;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.Abstractions.Index;
using Pure.RelationalSchema.Abstractions.Table;

namespace Pure.RelationalSchema.Random;

public sealed record RandomTable : ITable
{
    private readonly Lazy<IEnumerable<IColumn>> _lazyColumns;

    private readonly Lazy<IEnumerable<IIndex>> _lazyIndexes;

    public RandomTable()
        : this(new System.Random()) { }

    public RandomTable(System.Random random)
        : this(
            new CachedString(new RandomString(new RandomUShort(random), random)),
            new Lazy<IEnumerable<IColumn>>(() =>
                new RandomColumnsCollection(random).ToArray()
            ),
            new Lazy<IEnumerable<IIndex>>(() => new RandomIndexesCollection(random))
        )
    { }

    private RandomTable(
        IString name,
        Lazy<IEnumerable<IColumn>> lazyColumns,
        Lazy<IEnumerable<IIndex>> lazyIndexes
    )
    {
        Name = name;
        _lazyColumns = lazyColumns;
        _lazyIndexes = lazyIndexes;
    }

    public IString Name { get; }

    public IEnumerable<IColumn> Columns => _lazyColumns.Value;

    public IEnumerable<IIndex> Indexes => _lazyIndexes.Value;

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}

using Pure.Primitives.Abstractions.String;
using Pure.Primitives.Random.String;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.Abstractions.Index;
using Pure.RelationalSchema.Abstractions.Table;

namespace Pure.RelationalSchema.Random;

using Random = System.Random;

public sealed record RandomTable : ITable
{
    private readonly Lazy<IEnumerable<IColumn>> _lazyColumns;

    private readonly Lazy<IEnumerable<IIndex>> _lazyIndexes;

    public RandomTable()
        : this(Random.Shared) { }

    public RandomTable(Random random)
        : this(new RandomColumnsCollection(random), new RandomIndexesCollection(random))
    { }

    public RandomTable(
        RandomColumnsCollection randomColumns,
        RandomIndexesCollection randomIndexes
    )
        : this(randomColumns, randomIndexes, Random.Shared) { }

    public RandomTable(RandomColumnsCollection randomColumns)
        : this(randomColumns, Random.Shared) { }

    public RandomTable(RandomColumnsCollection randomColumns, Random random)
        : this(randomColumns, new RandomIndexesCollection(random), random) { }

    public RandomTable(RandomIndexesCollection randomIndexes)
        : this(randomIndexes, Random.Shared) { }

    public RandomTable(RandomIndexesCollection randomIndexes, Random random)
        : this(new RandomColumnsCollection(random), randomIndexes, random) { }

    public RandomTable(
        RandomColumnsCollection randomColumns,
        RandomIndexesCollection randomIndexes,
        Random random
    )
        : this(new RandomString(random), randomColumns, randomIndexes) { }

    public RandomTable(
        RandomString randomName,
        RandomColumnsCollection randomColumns,
        RandomIndexesCollection randomIndexes
    )
        // Stryker disable once linq
        : this(randomName, randomColumns.AsEnumerable(), randomIndexes.AsEnumerable()) { }

    internal RandomTable(
        IString name,
        IEnumerable<IColumn> columns,
        IEnumerable<IIndex> indexes
    )
        : this(
            name,
            new Lazy<IEnumerable<IColumn>>(columns.ToArray),
            new Lazy<IEnumerable<IIndex>>(indexes.ToArray)
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

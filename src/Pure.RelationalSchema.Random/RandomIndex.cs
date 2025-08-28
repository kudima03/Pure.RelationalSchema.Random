using Pure.Primitives.Abstractions.Bool;
using Pure.Primitives.Random.Bool;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.Abstractions.Index;

namespace Pure.RelationalSchema.Random;

using Random = System.Random;

public sealed record RandomIndex : IIndex
{
    private readonly Lazy<IEnumerable<IColumn>> _lazyColumns;

    public RandomIndex()
        : this(new Random()) { }

    public RandomIndex(Random random)
        : this(new RandomBool(random), new RandomColumnsCollection(random)) { }

    public RandomIndex(RandomColumnsCollection columns)
        : this(columns, Random.Shared) { }

    public RandomIndex(RandomColumnsCollection columns, Random random)
        : this(new RandomBool(random), columns) { }

    internal RandomIndex(IBool isUnique, IEnumerable<IColumn> columns)
        : this(isUnique, new Lazy<IEnumerable<IColumn>>(columns.ToArray)) { }

    private RandomIndex(IBool isUnique, Lazy<IEnumerable<IColumn>> columns)
    {
        IsUnique = isUnique;
        _lazyColumns = columns;
    }

    public IBool IsUnique { get; }

    public IEnumerable<IColumn> Columns => _lazyColumns.Value;

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}

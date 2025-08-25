using Pure.Primitives.Abstractions.Bool;
using Pure.Primitives.Number;
using Pure.Primitives.Random.Bool;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.Abstractions.Index;

namespace Pure.RelationalSchema.Random;

public sealed record RandomIndex : IIndex
{
    private readonly Lazy<IEnumerable<IColumn>> _lazyColumns;

    public RandomIndex()
        : this(new System.Random()) { }

    public RandomIndex(System.Random random)
        : this(
            new RandomBool(random),
            new Lazy<IEnumerable<IColumn>>(() =>
                new RandomColumnsCollection(new UShort(10), random).ToArray()
            )
        )
    { }

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

using Pure.Primitives.Abstractions.Bool;
using Pure.Primitives.Cached.Bool;
using Pure.Primitives.Random.Bool;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.Abstractions.Index;

namespace Pure.RelationalSchema.Random;

public sealed record RandomIndex : IIndex
{
    public RandomIndex()
        : this(new System.Random()) { }

    public RandomIndex(System.Random random)
        : this(new CachedBool(new RandomBool(random)), new RandomColumnsCollection(random)) { }

    private RandomIndex(IBool isUnique, IEnumerable<IColumn> columns)
    {
        IsUnique = isUnique;
        Columns = columns;
    }

    public IBool IsUnique { get; }

    public IEnumerable<IColumn> Columns { get; }

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}

using System.Collections;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Random.Number;
using Pure.RelationalSchema.Abstractions.Table;

namespace Pure.RelationalSchema.Random;

public sealed record RandomTablesCollection : IEnumerable<ITable>
{
    private readonly INumber<ushort> _count;

    private readonly RandomIndexesCollection _indexes;

    private readonly RandomColumnsCollection _columns;

    private readonly System.Random _random;

    public RandomTablesCollection()
        : this(System.Random.Shared) { }

    public RandomTablesCollection(System.Random random)
        : this(new RandomUShort(random), random) { }

    public RandomTablesCollection(INumber<ushort> count)
        : this(count, System.Random.Shared) { }

    public RandomTablesCollection(INumber<ushort> count, System.Random random)
        : this(
            count,
            new RandomColumnsCollection(random),
            new RandomIndexesCollection(random),
            random
        )
    { }

    public RandomTablesCollection(RandomColumnsCollection columns)
        : this(columns, System.Random.Shared) { }

    public RandomTablesCollection(RandomColumnsCollection columns, System.Random random)
        : this(
            new RandomUShort(random),
            columns,
            new RandomIndexesCollection(random),
            random
        )
    { }

    public RandomTablesCollection(RandomIndexesCollection indexes)
        : this(indexes, System.Random.Shared) { }

    public RandomTablesCollection(RandomIndexesCollection indexes, System.Random random)
        : this(
            new RandomUShort(random),
            new RandomColumnsCollection(random),
            indexes,
            random
        )
    { }

    public RandomTablesCollection(INumber<ushort> count, RandomColumnsCollection columns)
        : this(count, columns, System.Random.Shared) { }

    public RandomTablesCollection(
        INumber<ushort> count,
        RandomColumnsCollection columns,
        System.Random random
    )
        : this(count, columns, new RandomIndexesCollection(random), random) { }

    public RandomTablesCollection(INumber<ushort> count, RandomIndexesCollection indexes)
        : this(count, indexes, System.Random.Shared) { }

    public RandomTablesCollection(
        INumber<ushort> count,
        RandomIndexesCollection indexes,
        System.Random random
    )
        : this(count, new RandomColumnsCollection(random), indexes, random) { }

    public RandomTablesCollection(
        RandomColumnsCollection columns,
        RandomIndexesCollection indexes
    )
        : this(columns, indexes, System.Random.Shared) { }

    public RandomTablesCollection(
        RandomColumnsCollection columns,
        RandomIndexesCollection indexes,
        System.Random random
    )
        : this(new RandomUShort(random), columns, indexes, random) { }

    public RandomTablesCollection(
        INumber<ushort> count,
        RandomColumnsCollection columns,
        RandomIndexesCollection indexes
    )
        : this(count, columns, indexes, System.Random.Shared) { }

    public RandomTablesCollection(
        INumber<ushort> count,
        RandomColumnsCollection columns,
        RandomIndexesCollection indexes,
        System.Random random
    )
    {
        _count = count;
        _columns = columns;
        _random = random;
        _indexes = indexes;
    }

    public IEnumerator<ITable> GetEnumerator()
    {
        for (int i = 0; i < _count.NumberValue; i++)
        {
            yield return new RandomTable(_columns, _indexes, _random);
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

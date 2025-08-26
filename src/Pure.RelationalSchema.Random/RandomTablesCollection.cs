using System.Collections;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Random.Number;
using Pure.RelationalSchema.Abstractions.Table;

namespace Pure.RelationalSchema.Random;

using Random = System.Random;

public sealed record RandomTablesCollection : IEnumerable<ITable>
{
    private readonly INumber<ushort> _count;

    private readonly RandomIndexesCollection _indexes;

    private readonly RandomColumnsCollection _columns;

    private readonly Random _random;

    public RandomTablesCollection()
        : this(Random.Shared) { }

    public RandomTablesCollection(Random random)
        : this(new RandomUShort(random), random) { }

    public RandomTablesCollection(INumber<ushort> count)
        : this(count, Random.Shared) { }

    public RandomTablesCollection(INumber<ushort> count, Random random)
        : this(
            count,
            new RandomColumnsCollection(random),
            new RandomIndexesCollection(random),
            random
        )
    { }

    public RandomTablesCollection(RandomColumnsCollection columns)
        : this(columns, Random.Shared) { }

    public RandomTablesCollection(RandomColumnsCollection columns, Random random)
        : this(
            new RandomUShort(random),
            columns,
            new RandomIndexesCollection(random),
            random
        )
    { }

    public RandomTablesCollection(RandomIndexesCollection indexes)
        : this(indexes, Random.Shared) { }

    public RandomTablesCollection(RandomIndexesCollection indexes, Random random)
        : this(
            new RandomUShort(random),
            new RandomColumnsCollection(random),
            indexes,
            random
        )
    { }

    public RandomTablesCollection(INumber<ushort> count, RandomColumnsCollection columns)
        : this(count, columns, Random.Shared) { }

    public RandomTablesCollection(
        INumber<ushort> count,
        RandomColumnsCollection columns,
        Random random
    )
        : this(count, columns, new RandomIndexesCollection(random), random) { }

    public RandomTablesCollection(INumber<ushort> count, RandomIndexesCollection indexes)
        : this(count, indexes, Random.Shared) { }

    public RandomTablesCollection(
        INumber<ushort> count,
        RandomIndexesCollection indexes,
        Random random
    )
        : this(count, new RandomColumnsCollection(random), indexes, random) { }

    public RandomTablesCollection(
        RandomColumnsCollection columns,
        RandomIndexesCollection indexes
    )
        : this(columns, indexes, Random.Shared) { }

    public RandomTablesCollection(
        RandomColumnsCollection columns,
        RandomIndexesCollection indexes,
        Random random
    )
        : this(new RandomUShort(random), columns, indexes, random) { }

    public RandomTablesCollection(
        INumber<ushort> count,
        RandomColumnsCollection columns,
        RandomIndexesCollection indexes
    )
        : this(count, columns, indexes, Random.Shared) { }

    public RandomTablesCollection(
        INumber<ushort> count,
        RandomColumnsCollection columns,
        RandomIndexesCollection indexes,
        Random random
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

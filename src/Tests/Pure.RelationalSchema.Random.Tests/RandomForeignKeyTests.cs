using Pure.HashCodes;
using Pure.Primitives.Number;
using Pure.RelationalSchema.Abstractions.ForeignKey;
using Pure.RelationalSchema.HashCodes;

namespace Pure.RelationalSchema.Random.Tests;

using Random = System.Random;

public sealed record RandomForeignKeyTests
{
    [Fact]
    public void InitializeReferencingTableAsCached()
    {
        RandomTable table = new RandomTable();

        IForeignKey foreignKey = new RandomForeignKey(
            table,
            new RandomColumnsCollection(new UShort(1)),
            new RandomTable(),
            new RandomColumnsCollection(new UShort(1))
        );

        Assert.Equal(
            new TableHash(foreignKey.ReferencingTable),
            new TableHash(foreignKey.ReferencingTable),
            new DeterminedHashEqualityComparer()
        );
    }

    [Fact]
    public void InitializeReferencedTableAsCached()
    {
        RandomTable table = new RandomTable();

        IForeignKey foreignKey = new RandomForeignKey(
            new RandomTable(),
            new RandomColumnsCollection(new UShort(1)),
            table,
            new RandomColumnsCollection(new UShort(1))
        );

        Assert.Equal(
            new TableHash(foreignKey.ReferencedTable),
            new TableHash(foreignKey.ReferencedTable),
            new DeterminedHashEqualityComparer()
        );
    }

    [Fact]
    public void InitializeReferencingColumnAsCached()
    {
        IForeignKey foreignKey = new RandomForeignKey();

        Assert.Equal(
            new AggregatedHash(
                foreignKey.ReferencingColumns.Select(x => new ColumnHash(x))
            ),
            new AggregatedHash(
                foreignKey.ReferencingColumns.Select(x => new ColumnHash(x))
            ),
            new DeterminedHashEqualityComparer()
        );
    }

    [Fact]
    public void InitializeReferencedColumnAsCached()
    {
        IForeignKey foreignKey = new RandomForeignKey();

        Assert.Equal(
            new AggregatedHash(
                foreignKey.ReferencedColumns.Select(x => new ColumnHash(x))
            ),
            new AggregatedHash(
                foreignKey.ReferencedColumns.Select(x => new ColumnHash(x))
            ),
            new DeterminedHashEqualityComparer()
        );
    }

    [Fact]
    public void ProduceRandomValuesWithSharedProvider()
    {
        const int count = 5;

        Random random = new Random();

        IEnumerable<IForeignKey> randoms = Enumerable
            .Range(0, count)
            .Select(_ => new RandomForeignKey(random));

        Assert.Equal(
            count,
            randoms
                .Select(x => new ForeignKeyHash(x))
                .Distinct(new DeterminedHashEqualityComparer())
                .Count()
        );
    }

    [Fact]
    public void ProduceRandomValues()
    {
        const int count = 5;

        IEnumerable<IForeignKey> randoms = Enumerable
            .Range(0, count)
            .Select(_ => new RandomForeignKey());

        Assert.Equal(
            count,
            randoms
                .Select(x => new ForeignKeyHash(x))
                .Distinct(new DeterminedHashEqualityComparer())
                .Count()
        );
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new RandomForeignKey().GetHashCode()
        );
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() => new RandomForeignKey().ToString());
    }
}

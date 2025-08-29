using Pure.HashCodes;
using Pure.Primitives.Number;
using Pure.Primitives.Random.String;
using Pure.RelationalSchema.Abstractions.Table;
using Pure.RelationalSchema.HashCodes;

namespace Pure.RelationalSchema.Random.Tests;

using Random = System.Random;

public sealed record RandomTableTests
{
    [Fact]
    public void DefaultConstructorProduceNameLessThan100Symbols()
    {
        IEnumerable<ITable> randoms = Enumerable
            .Range(0, 100)
            .Select(_ => new RandomTable());

        Assert.True(randoms.All(x => x.Name.TextValue.Length < 100));
    }

    [Fact]
    public void InitializeNameAsCached()
    {
        ITable table = new RandomTable();

        Assert.Equal(
            new DeterminedHash(table.Name),
            new DeterminedHash(table.Name),
            new DeterminedHashEqualityComparer()
        );
    }

    [Fact]
    public void InitializeColumnsAsCached()
    {
        ITable table = new RandomTable();

        Assert.Equal(
            new AggregatedHash(table.Columns.Select(x => new ColumnHash(x))),
            new AggregatedHash(table.Columns.Select(x => new ColumnHash(x))),
            new DeterminedHashEqualityComparer()
        );
    }

    [Fact]
    public void InitializeIndexesAsCached()
    {
        ITable table = new RandomTable();

        Assert.Equal(
            new AggregatedHash(table.Indexes.Select(x => new IndexHash(x))),
            new AggregatedHash(table.Indexes.Select(x => new IndexHash(x))),
            new DeterminedHashEqualityComparer()
        );
    }

    [Fact]
    public void ProduceRandomValuesWithSharedProvider()
    {
        const int count = 5;

        Random random = new Random();

        IEnumerable<ITable> randoms = Enumerable
            .Range(0, count)
            .Select(_ => new RandomTable(random));

        Assert.Equal(
            count,
            randoms
                .Select(x => new TableHash(x))
                .Distinct(new DeterminedHashEqualityComparer())
                .Count()
        );
    }

    [Fact]
    public void ProduceRandomValues()
    {
        const int count = 5;

        IEnumerable<ITable> randoms = Enumerable
            .Range(0, count)
            .Select(_ => new RandomTable());

        Assert.Equal(
            count,
            randoms
                .Select(x => new TableHash(x))
                .Distinct(new DeterminedHashEqualityComparer())
                .Count()
        );
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() => new RandomTable().GetHashCode());
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() => new RandomTable().ToString());
    }
}

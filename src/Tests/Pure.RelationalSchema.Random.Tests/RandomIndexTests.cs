using Pure.HashCodes;
using Pure.RelationalSchema.Abstractions.Index;
using Pure.RelationalSchema.HashCodes;

namespace Pure.RelationalSchema.Random.Tests;

public sealed record RandomIndexTests
{
    [Fact]
    public void InitializeColumnsAsCached()
    {
        IIndex index = new RandomIndex();

        Assert.Equal(
            new AggregatedHash(index.Columns.Select(x => new ColumnHash(x))),
            new AggregatedHash(index.Columns.Select(x => new ColumnHash(x))),
            new DeterminedHashEqualityComparer()
        );
    }

    [Fact]
    public void InitializeUniquenessAsCached()
    {
        IIndex index = new RandomIndex();

        Assert.Equal(
            new DeterminedHash(index.IsUnique),
            new DeterminedHash(index.IsUnique),
            new DeterminedHashEqualityComparer()
        );
    }

    [Fact]
    public void ProduceRandomValuesWithSharedProvider()
    {
        const int count = 100;

        System.Random random = new System.Random();

        IEnumerable<IIndex> randoms = Enumerable
            .Range(0, count)
            .Select(_ => new RandomIndex(random));

        Assert.Equal(
            count,
            randoms
                .Select(x => new IndexHash(x))
                .Distinct(new DeterminedHashEqualityComparer())
                .Count()
        );
    }

    [Fact]
    public void ProduceRandomValues()
    {
        const int count = 100;

        IEnumerable<IIndex> randoms = Enumerable
            .Range(0, count)
            .Select(_ => new RandomIndex());

        Assert.Equal(
            count,
            randoms
                .Select(x => new IndexHash(x))
                .Distinct(new DeterminedHashEqualityComparer())
                .Count()
        );
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() => new RandomIndex().GetHashCode());
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() => new RandomIndex().ToString());
    }
}

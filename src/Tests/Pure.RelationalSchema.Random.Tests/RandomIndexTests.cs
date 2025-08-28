using Pure.HashCodes;
using Pure.Primitives.Number;
using Pure.Primitives.Random.String;
using Pure.RelationalSchema.Abstractions.Index;
using Pure.RelationalSchema.HashCodes;

namespace Pure.RelationalSchema.Random.Tests;

using Random = System.Random;

public sealed record RandomIndexTests
{
    [Fact]
    public void InitializeColumnsAsCached()
    {
        IIndex index = new RandomIndex(new RandomColumnsCollection(new UShort(5)));

        Assert.Equal(
            new AggregatedHash(index.Columns.Select(x => new ColumnHash(x))),
            new AggregatedHash(index.Columns.Select(x => new ColumnHash(x))),
            new DeterminedHashEqualityComparer()
        );
    }

    [Fact]
    public void InitializeUniquenessAsCached()
    {
        IIndex index = new RandomIndex(new RandomColumnsCollection(new UShort(5)));

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

        Random random = new Random();

        IEnumerable<IIndex> randoms = Enumerable
            .Range(0, count)
            .Select(_ => new RandomIndex(
                new RandomColumnsCollection(
                    new UShort(5),
                    new RandomStringCollection(new UShort(10), new UShort(10), random),
                    new RandomColumnTypesCollection(
                        new UShort(10),
                        new RandomStringCollection(new UShort(10), new UShort(10), random)
                    )
                ),
                random
            ));

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
            .Select(_ => new RandomIndex(
                new RandomColumnsCollection(
                    new UShort(5),
                    new RandomStringCollection(new UShort(10), new UShort(10)),
                    new RandomColumnTypesCollection(
                        new UShort(10),
                        new RandomStringCollection(new UShort(10), new UShort(10))
                    )
                )
            ));

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
